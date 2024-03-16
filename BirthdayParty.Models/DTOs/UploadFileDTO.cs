using Microsoft.AspNetCore.Http;

namespace BirthdayParty.Models.DTOs;
public class UploadFileDTO{
    public required IFormFile File { get; set; }
    public bool IsUploadingImage {get;set;}
}
