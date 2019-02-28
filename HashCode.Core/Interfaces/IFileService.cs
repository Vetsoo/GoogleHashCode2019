using HashCode.Core.Domain;

namespace HashCode.Core.Interfaces
{
    public interface IFileService
    {
        Collection ReadFile(string fileName);
        void WriteFile(string inputFile, Result result);
    }
}
