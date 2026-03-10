using System;

namespace my_online_portfolio.Models;

public class Gallery
{
  public int Id { get; set; }
  public string Title { get; set; }
  public string ImagePath { get; set; }
  public DateTime UploadedAt { get; set; }
}