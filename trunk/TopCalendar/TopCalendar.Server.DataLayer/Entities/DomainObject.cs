namespace TopCalendar.Server.DataLayer.Entities
{
	public abstract class DomainObject<TPk>		
	{
		public virtual TPk Id { get; private set; }		
	}
}