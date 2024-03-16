using Microsoft.AspNetCore.Http;

namespace BirthdayParty.Repository;

public class FileConvertUtils
{
    private static readonly long ONE_MEGABYTE = 1024 * 1024;
    private static readonly long FILE_SIZE_LIMIT = 10 * ONE_MEGABYTE; //10MB
    
    public FileConvertUtils()
    {
    }

    public static byte[] ConvertToByteArray(IFormFile file)
    {
        if (file == null) throw new ArgumentNullException(nameof(file));
        if (file.Length > FILE_SIZE_LIMIT)
        {
            throw new Exception($"File size larger than {FILE_SIZE_LIMIT / ONE_MEGABYTE}MB");
        }
        //convert file to byte[]
        byte[] img = null;
        using (var stream = file.OpenReadStream())
        using (var ms = new MemoryStream())
        {
                stream.CopyTo(ms);
                img = ms.ToArray();
        }

        return img;
    }
}
