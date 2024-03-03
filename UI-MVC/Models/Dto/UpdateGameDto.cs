using System.ComponentModel.DataAnnotations;
using StoreManagement.BL.Domain;

namespace StoreManagement.UI.Web.MVC.Models.Dto;

public class UpdateGameDto 
{
    public int Id { get; set; }
    public int Rating { get; set; }
    
}