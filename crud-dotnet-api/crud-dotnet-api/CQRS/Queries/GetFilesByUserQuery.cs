using MediatR;
using crud_dotnet_api.Model;
using System;
using System.Collections.Generic;

namespace crud_dotnet_api.CQRS.Queries
{
    public class GetFilesByUserQuery : IRequest<List<UploadedFileModel>>
    {
        public Guid CreatedBy { get; set; }
    }
}