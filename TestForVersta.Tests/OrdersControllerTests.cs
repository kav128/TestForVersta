using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TestForVersta.BLL.Services;
using TestForVersta.Controllers;
using TestForVersta.Models;

namespace TestForVersta.Tests;

public class OrdersControllerTests
{
    private Mock<ILogger<OrdersController>> _loggerMock;
    private Mock<IOrderService> _orderServiceMock;
    private Mock<IMapper> _mapperMock;
    private Mock<IValidator<OrderInsertModel>> _validatorMock;

    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<OrdersController>>();
        _orderServiceMock = new Mock<IOrderService>();
        _mapperMock = new Mock<IMapper>();
        _validatorMock = new Mock<IValidator<OrderInsertModel>>();
    }

    [Test]
    public async Task Get_Index_Test_Success()
    {
        var orders = new List<BLL.Models.Order>();
        var expected = new List<Order>();

        _orderServiceMock.Setup(service => service.GetOrders(It.IsAny<CancellationToken>())).ReturnsAsync(orders);
        _mapperMock.Setup(mapper => mapper.Map<IList<Order>>(orders)).Returns(expected);

        var ordersController = new OrdersController(_loggerMock.Object,
                                                    _orderServiceMock.Object,
                                                    _mapperMock.Object,
                                                    _validatorMock.Object);
        var actionResult = await ordersController.Index() as ViewResult;

        Assert.That(actionResult, Is.Not.Null, "ActionResult is not instance of ViewResult");
        Assert.That(actionResult!.Model, Is.EqualTo(expected));
        Assert.That(actionResult.ViewName, Is.Null.Or.EqualTo("Index"));

        _orderServiceMock.Verify(service => service.GetOrders(It.IsAny<CancellationToken>()), Times.Once);
        _orderServiceMock.VerifyNoOtherCalls();

        _mapperMock.Verify(mapper => mapper.Map<IList<Order>>(orders), Times.Once);
        _mapperMock.VerifyNoOtherCalls();
    }

    [Test]
    public void Get_Add_Test_Success()
    {
        var ordersController = new OrdersController(_loggerMock.Object,
                                                    _orderServiceMock.Object,
                                                    _mapperMock.Object,
                                                    _validatorMock.Object);

        var actionResult = ordersController.Add() as ViewResult;

        Assert.That(actionResult, Is.Not.Null, "ActionResult is not instance of ViewResult");
        Assert.That(actionResult!.Model, Is.Null);
        Assert.That(actionResult.ViewName, Is.Null.Or.EqualTo("Add"));
    }

    [Test]
    public async Task Post_Add_Test_Success()
    {
        var model = new OrderInsertModel
        {
            SenderCity = "TS1",
            ReceiverCity = "TS2",
            SenderAddress = "TA1",
            ReceiverAddress = "TA2",
            Weight = 5,
            DeliveryDate = new DateTime(2021, 1, 5)
        };

        var modelBll = new BLL.Models.OrderInsertModel
        {
            SenderCity = "TS1",
            ReceiverCity = "TS2",
            SenderAddress = "TA1",
            ReceiverAddress = "TA2",
            Weight = 5,
            DeliveryDate = new DateTime(2021, 1, 5)
        };

        _validatorMock.Setup(validator => validator.ValidateAsync(It.IsAny<OrderInsertModel>(),
                                                                  It.IsAny<CancellationToken>()))
                      .ReturnsAsync(new ValidationResult());

        _mapperMock.Setup(mapper => mapper.Map<BLL.Models.OrderInsertModel>(model)).Returns(modelBll);

        var ordersController = new OrdersController(_loggerMock.Object,
                                                    _orderServiceMock.Object,
                                                    _mapperMock.Object,
                                                    _validatorMock.Object);

        var actionResult = await ordersController.Add(model) as RedirectToActionResult;

        Assert.That(actionResult, Is.Not.Null, "ActionResult is not instance of RedirectResult");
        Assert.That(actionResult!.ActionName, Is.EqualTo("Index"));

        _validatorMock.Verify(validator => validator.ValidateAsync(model, It.IsAny<CancellationToken>()), Times.Once);
        _validatorMock.VerifyNoOtherCalls();

        _mapperMock.Verify(mapper => mapper.Map<BLL.Models.OrderInsertModel>(model), Times.Once);
        _mapperMock.VerifyNoOtherCalls();

        _orderServiceMock.Verify(service => service.AddOrder(modelBll, It.IsAny<CancellationToken>()), Times.Once);
        _orderServiceMock.VerifyNoOtherCalls();
    }

    [Test]
    public async Task Post_Add_Test_Invalid()
    {
        var model = new OrderInsertModel
        {
            SenderCity = "TS1",
            ReceiverCity = "TS2",
            SenderAddress = "TA1",
            ReceiverAddress = "TA2",
            Weight = -5,
            DeliveryDate = new DateTime(2015, 1, 5)
        };

        var validationResult = new ValidationResult
        {
            Errors = new List<ValidationFailure>
            {
                new("Weight", "Error 1"),
                new("DeliveryDate", "Error 2")
            }
        };

        _validatorMock.Setup(validator => validator.ValidateAsync(model, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(validationResult);


        var ordersController = new OrdersController(_loggerMock.Object,
                                                    _orderServiceMock.Object,
                                                    _mapperMock.Object,
                                                    _validatorMock.Object);

        var actionResult = await ordersController.Add(model) as ViewResult;

        Assert.That(actionResult, Is.Not.Null, "ActionResult is not instance of ViewResult");
        Assert.That(actionResult!.Model, Is.EqualTo(model));
        Assert.That(actionResult.ViewName, Is.Null.Or.EqualTo("Add"));

        _validatorMock.Verify(validator => validator.ValidateAsync(model, It.IsAny<CancellationToken>()), Times.Once);
        _validatorMock.VerifyNoOtherCalls();
        _mapperMock.VerifyNoOtherCalls();
        _orderServiceMock.VerifyNoOtherCalls();
    }
}
