﻿using Microsoft.AspNetCore.Mvc;
using TestForVersta.BLL.Services;
using TestForVersta.Models;

namespace TestForVersta.Controllers;

public class OrdersController : Controller
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IOrderService _orderService;

    public OrdersController(ILogger<OrdersController> logger, IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }

    public async Task<IActionResult> Index()
    {
        var orders = (await _orderService.GetOrders()).Select(order => new Order
                                                       {
                                                           Id = order.Id,
                                                           SenderCity = order.SenderCity,
                                                           SenderAddress = order.SenderAddress,
                                                           ReceiverCity = order.ReceiverCity,
                                                           ReceiverAddress = order.ReceiverAddress,
                                                           Weight = order.Weight,
                                                           DeliveryDate = order.DeliveryDate
                                                       })
                                                      .ToList();
        return View(orders);
    }
    
    public IActionResult Add()
    {
        return View();
    }
    
    
    [HttpPost]
    public IActionResult Add(OrderInsertModel model)
    {
        var orderInsertModel = new BLL.Models.OrderInsertModel
        {
            SenderCity = model.SenderCity,
            SenderAddress = model.SenderAddress,
            ReceiverCity = model.ReceiverCity,
            ReceiverAddress = model.ReceiverAddress,
            Weight = model.Weight,
            DeliveryDate = model.DeliveryDate
        };
        
        _orderService.AddOrder(orderInsertModel);
        return RedirectToAction("Index");
    }
}
