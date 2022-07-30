namespace TestForVersta.Models;

/// <summary>
/// Represents an order.
/// </summary>
public record Order
{
    /// <summary>
    /// Order's unique identifier.
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// The sender's city.
    /// </summary>
    public string SenderCity { get; set; }
    
    /// <summary>
    /// The sender's address.
    /// </summary>
    public string SenderAddress { get; set; }
    
    /// <summary>
    /// The receiver's city.
    /// </summary>
    public string ReceiverCity { get; set; }
    
    /// <summary>
    /// The receiver's address.
    /// </summary>
    public string ReceiverAddress { get; set; }
    
    /// <summary>
    /// Weight of the parcel in kilograms.
    /// </summary>
    public double Weight { get; set; }
    
    /// <summary>
    /// Date of delivery.
    /// </summary>
    public DateTime DeliveryDate { get; set; }
}
