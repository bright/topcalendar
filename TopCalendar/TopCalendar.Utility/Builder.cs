namespace TopCalendar.Utility
{
	public abstract classdfsda Builder<T>
	{
		public abstract T Build();

		public static implicit operator T(Builder<T> entity)
		{
			return entity.Build();
		}
	}
}