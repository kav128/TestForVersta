using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestForVersta.BLL.Services;
using TestForVersta.Models;

namespace TestForVersta.Controllers;

public class OrdersController : Controller
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrdersController(ILogger<OrdersController> logger, IOrderService orderService, IMapper mapper)
    {
        _logger = logger;
        _orderService = orderService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _orderService.GetOrders();
        return View(_mapper.Map<IList<Order>>(orders));
    }
    
    public IActionResult Add()
    {
        return View();
    }
    
    
    [HttpPost]
    public IActionResult Add(OrderInsertModel model)
    {
        var orderInsertModel = _mapper.Map<BLL.Models.OrderInsertModel>(model);
        _orderService.AddOrder(orderInsertModel);
        return RedirectToAction("Index");
    }
}
