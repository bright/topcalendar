using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite.Events;
using NUnit.Framework;
using TopCalendar.Utility.Tests;
using Microsoft.Practices.Composite.Presentation.Events;

namespace Composite.Desktop.EventAggregatorSpec
{

    /// <summary>
    /// 
    /// Poniższa specyfikacja demonstruje działanie EventAggregator-a. Działąnie w naszej aplikacji jest analogiczne, z tym, że
    /// event aggregatora dostaje sie z Unity ( trzeba poprosić o IEventAggregator).
    /// 
    /// Należy pamiętać o Unsubscribe jeżeli zrobimy Subscribe z parametrem keepSubscriberReferenceAlive = true
    /// 
    /// Chyba się to nikomu i tak nie przyda :)
    /// 
    /// </summary>

    public abstract class EventAggregatorTestBase : observations_for_sut_of_type<IEventAggregator>
    {
        protected readonly static string ArgName = "some arg name";
        protected readonly static string ArgValue = "some arg value";

        protected SomeArg _arg = new SomeArg { Name = ArgName, Value = ArgValue };


        protected override IEventAggregator CreateSut()
        {
            return new EventAggregator();
        }

        protected override void Because()
        {
            Sut.GetEvent<SomeEvent>().Publish(_arg);
        }
    }

    [TestFixture]
    public class publish_event_with_2_subscribers : EventAggregatorTestBase
    {

        protected string subscriber1Prefix = "s1:";
        protected string subscriber2Prefix = "s2:";

        protected string subscriber1Result;
        protected string subscriber2Result;

        protected string Subscriber1ExpectedResult
        {
            get { return subscriber1Prefix + _arg.Name; }
        }

        protected string Subscriber2ExpectedResult
        {
            get { return subscriber2Prefix + _arg.Name; }
        }

        protected override void AfterSutCreation()
        {
            Sut.GetEvent<SomeEvent>().Subscribe(x => subscriber1Result = subscriber1Prefix + x.Name);
            Sut.GetEvent<SomeEvent>().Subscribe(x => subscriber2Result = subscriber2Prefix + x.Name);
        }

        [Test]
        public void all_aubscribers_were_called()
        {
            Assert.AreEqual(Subscriber1ExpectedResult, subscriber1Result);
            Assert.AreEqual(Subscriber2ExpectedResult, subscriber2Result);
        }
    }

    [TestFixture]
    public class publish_event_with_subscriber_that_has_predicate_and_predicate_return_false : EventAggregatorTestBase
    {
        protected string correctName = "correctName";
        protected bool subscriberWasCalled = false;

        protected string expectedResult = "expectedResult";

        protected override void AfterSutCreation()
        {
            Sut.GetEvent<SomeEvent>().Subscribe(x => { subscriberWasCalled = true; }, ThreadOption.BackgroundThread, false,
                                                x => false);
        }

        [Test]
        public void subscriber_action_should_not_be_called()
        {
            Assert.IsFalse(subscriberWasCalled);
        }
    }

    [TestFixture]
    public class publish_event_with_subscriber_that_has_predicate_and_predicate_return_true : EventAggregatorTestBase
    {
        protected string correctName = "correctName";
        protected bool subscriberWasCalled;

        protected override void EstablishContext()
        {
            base.EstablishContext();
            subscriberWasCalled = false;
        }

        protected string expectedResult = "expectedResult";

        protected override void AfterSutCreation()
        {
            Sut.GetEvent<SomeEvent>().Subscribe(x => { subscriberWasCalled = true; }, ThreadOption.PublisherThread,
                false,  // if true you should self release reference(unsubscribe)
                x => true);
        }

        [Test]
        public void subscriber_action_should_be_called()
        {
            Assert.IsTrue(subscriberWasCalled);
        }
    }

    [TestFixture]
    public class when_subscribe_using_hard_binding : EventAggregatorTestBase
    {
        protected SubscriptionToken subscriptionToken;
        protected int actionCalledCounter;

        protected string expectedArgValueAfterOnePublish = ArgValue + ArgValue;

        protected override void AfterSutCreation()
        {
            subscriptionToken = Sut.GetEvent<SomeEvent>().Subscribe(SomeEventHandler, ThreadOption.PublisherThread, true);
        }

        protected override void EstablishContext()
        {
            base.EstablishContext();
            actionCalledCounter = 0;
        }

        [Test]
        public void unsubscribe_using_subscribe_token()
        {
            Action unsubscribe = () => Sut.GetEvent<SomeEvent>().Unsubscribe(subscriptionToken);
            AssertUnsubscribeWorkCorrect(unsubscribe);
        }

        [Test]
        public void unsubscribe_using_subscribe_method()
        {
            Action unsubscribe = () => Sut.GetEvent<SomeEvent>().Unsubscribe(SomeEventHandler);
            AssertUnsubscribeWorkCorrect(unsubscribe);
        }

        protected void AssertUnsubscribeWorkCorrect(Action unsubscribeAction)
        {
            Assert.AreEqual(1, actionCalledCounter);

            unsubscribeAction();

            Sut.GetEvent<SomeEvent>().Publish(_arg);

            Assert.AreEqual(1, actionCalledCounter);
        }

        protected void SomeEventHandler(SomeArg arg)
        {
            actionCalledCounter++;
        }

    }

    public class SomeEvent : CompositePresentationEvent<SomeArg>
    {

    }

    public class SomeArg
    {
        public string Name { get; set; }

        public string Value { get; set; }

    }
}
