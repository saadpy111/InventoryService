﻿using Inventory.Application.Contracts.Persistence.Repositories;
using Inventory.Application.Features.ProCategoryFeatures.Commands.UpdateCategory;
using Inventory.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Features.ProCategoryFeatures.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest, DeleteCategoryCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
             _unitOfWork = unitOfWork;
        }
        public async Task<DeleteCategoryCommandResponse> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _unitOfWork.Repositories<ProductCategory>().GetById(request.Id);
                if (category == null)
                    return new DeleteCategoryCommandResponse()
                    {
                          Success = false,
                          Message = "الصنف غير موجود"
                    };
              _unitOfWork.Repositories<ProductCategory>().Remove(category);
                await _unitOfWork.CompleteAsync();
                return new DeleteCategoryCommandResponse()
                {
                    Success = true,
                    Message = "تم بنجاح"
                };
            }
            catch
            {
                return new DeleteCategoryCommandResponse()
                {
                    Success = false,
                    Message = "حدث خطأ ما"
                };
            }
        }
    }
}
