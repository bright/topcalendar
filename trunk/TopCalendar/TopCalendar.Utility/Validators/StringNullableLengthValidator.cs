using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace TopCalendar.Utility.Validators
{
	/// <summary>
	/// Returns true if either validation target is null or matches stringLength validator
	/// </summary>
	public class StringNullableLengthValidator : StringLengthValidator
	{
		public StringNullableLengthValidator(int upperBound) : base(upperBound)
		{
		}

		public StringNullableLengthValidator(int upperBound, bool negated) : base(upperBound, negated)
		{
		}

		public StringNullableLengthValidator(int lowerBound, int upperBound) : base(lowerBound, upperBound)
		{
		}

		public StringNullableLengthValidator(int lowerBound, int upperBound, bool negated) : base(lowerBound, upperBound, negated)
		{
		}

		public StringNullableLengthValidator(int lowerBound, RangeBoundaryType lowerBoundType, int upperBound, RangeBoundaryType upperBoundType) : base(lowerBound, lowerBoundType, upperBound, upperBoundType)
		{
		}

		public StringNullableLengthValidator(int lowerBound, RangeBoundaryType lowerBoundType, int upperBound, RangeBoundaryType upperBoundType, bool negated) : base(lowerBound, lowerBoundType, upperBound, upperBoundType, negated)
		{
		}

		public StringNullableLengthValidator(int lowerBound, RangeBoundaryType lowerBoundType, int upperBound, RangeBoundaryType upperBoundType, string messageTemplate) : base(lowerBound, lowerBoundType, upperBound, upperBoundType, messageTemplate)
		{
		}

		public StringNullableLengthValidator(int lowerBound, RangeBoundaryType lowerBoundType, int upperBound, RangeBoundaryType upperBoundType, string messageTemplate, bool negated) : base(lowerBound, lowerBoundType, upperBound, upperBoundType, messageTemplate, negated)
		{
		}

		protected override void DoValidate(object objectToValidate, object currentTarget, string key, ValidationResults validationResults)
		{
			string toValidate = objectToValidate as string;
			DoValidate(toValidate,currentTarget,key,validationResults);
		}

		protected override void DoValidate(string objectToValidate, object currentTarget, string key, ValidationResults validationResults)
		{
			if (null == objectToValidate)
			{
				if (Negated)
					LogValidationResult(validationResults, MessageTemplate, currentTarget, key);
			}
			else
			{
				base.DoValidate(objectToValidate, currentTarget, key, validationResults);
			}		
		}
	}

	[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = true, Inherited = false)] 
	public class StringNullableLengthValidatorAttribute : ValueValidatorAttribute
	{
		private readonly int _lowerBound;
		private readonly RangeBoundaryType _lowerBoundType;
		private readonly int _upperBound;
		private readonly RangeBoundaryType _upperBoundType;

		public StringNullableLengthValidatorAttribute(int lowerBound, RangeBoundaryType lowerBoundType, int upperBound, RangeBoundaryType upperBoundType)
		{
			_lowerBound = lowerBound;
			_lowerBoundType = lowerBoundType;
			_upperBound = upperBound;
			_upperBoundType = upperBoundType;
		}

		public StringNullableLengthValidatorAttribute(int lowerBound, int upperBound):this(lowerBound,RangeBoundaryType.Inclusive,upperBound,RangeBoundaryType.Inclusive)
		{			
		}

		public StringNullableLengthValidatorAttribute(int upperBound)
			: this(0,RangeBoundaryType.Ignore,upperBound,RangeBoundaryType.Inclusive)
		{
		}


		protected override Validator DoCreateValidator(Type targetType)
		{
			return new StringNullableLengthValidator(_lowerBound, _lowerBoundType, _upperBound, _upperBoundType);
		}
	}
}