namespace SoccerAPI.Database.Models.BaseModels
{
	using System;

	public abstract class BaseModel
	{
		protected BaseModel()
		{
			this.Id = Guid.NewGuid();
			this.CreatedOn = DateTime.UtcNow;
			this.UpdatedOn = null;
		}

		public Guid Id { get; set; }

		public DateTime CreatedOn { get; set; }

		public DateTime? UpdatedOn { get; set; }
	}
}
