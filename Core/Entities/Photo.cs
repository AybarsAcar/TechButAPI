namespace Core.Entities
{
  /// <summary>
  /// Photo metadata stored in the database
  /// Actual image file is stored in file storage - Cloudinary
  /// </summary>
  public class Photo
  {
    // publicId we get from cloudinary
    public string Id { get; set; }

    // url we get from cloudinary
    public string Url { get; set; }

    // main photo is displayed on the home screen
    public bool IsMain { get; set; }
  }
}