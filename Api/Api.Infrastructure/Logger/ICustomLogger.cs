namespace Api.Infrastructure.Logger;

public interface ICustomLogger
{
    string LogPath { get; set; }
    NLog.Logger Manager { get; set; }
}