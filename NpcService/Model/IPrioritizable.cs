namespace NpcService.Model
{
    public interface IPrioritizable
    {
        /// <summary>
        /// Priority of the item.
        /// </summary>
        double Priority { get; set; }
    }
}