﻿
namespace EventManager.FileRepository.Models;

public class FileStorageOptions
{
    public string? EventRepositoryPath {  get; set; }
    public string? UserRepositoryPath { get; set; }
    public string? SubscriptionRepositoryPath { get; set; }
}
