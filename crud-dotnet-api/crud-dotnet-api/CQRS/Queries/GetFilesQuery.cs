
using MediatR;
using crud_dotnet_api.Model;
using System.Collections.Generic;
using global::crud_dotnet_api.Model;

namespace crud_dotnet_api.CQRS.Queries
{
    public class GetFilesQuery : IRequest<List<UploadedFileModel>>
    {
    }
}
