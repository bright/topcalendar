using TopCalendar.Utility.UI;

namespace TopCalendar.Utility.Tests.UI
{
	public abstract class observations_for_presentation_model_with_stubbed_view<TPresentationModel, TViewType>
		: observations_for_auto_created_sut_of_type_with_eventaggregator<TPresentationModel>
		where TPresentationModel : PresentationModelFor<TViewType>
		where TViewType : IViewForModel<TViewType,TPresentationModel>
	{
		protected override void EstablishContext()
		{
			ProvideImplementationOf(Stub<TViewType>());
			base.EstablishContext();
		}
	}
}