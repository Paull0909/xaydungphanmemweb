using Application.SeedWorks;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Web.Extension
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            return View(categories); // Hiển thị View ở /Views/Shared/Components/Category/Default.cshtml
        }

    }

}
