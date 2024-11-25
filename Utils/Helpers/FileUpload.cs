namespace onboarding_dotnet.Utils.Helpers;

class FileUpload
{
    public static async Task<string> Upload(IFormFile file, string baseUrl)
    {
        // Ensure directory exists
        var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Generate unique filename
        var filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var path = Path.Combine(directory, filename);

        // Save file
        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return $"{baseUrl}/uploads/{filename}";
    }

    public static void Delete(string path)
    {   
        // Get current directory
        var directory = Directory.GetCurrentDirectory();

        // Split path to get filename
        var parts = path.Split("/");
        var filename = parts[parts.Length - 1];

        path = Path.Combine(directory, "wwwroot", "uploads", filename);

        // Delete file
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public static void Validate(IFormFile file, string[] allowedExtensions, long maxSize)
    {   
        // Validate file is required
        if (file == null || file.Length == 0)
        {
            throw new BadHttpRequestException("File is required");
        }

        // Validate file size
        if (file.Length > maxSize)
        {
            throw new BadHttpRequestException("File size is too large");
        }

        // Validate file extension
        var extension = Path.GetExtension(file.FileName).ToLower();
        if (!allowedExtensions.Contains(extension))
        {
            throw new BadHttpRequestException("File extension is not allowed");
        }
    }
}