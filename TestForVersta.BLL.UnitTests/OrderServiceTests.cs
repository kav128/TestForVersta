using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using TestForVersta.BLL.Services;
using TestForVersta.DAL.Repositories;

namespace TestForVersta.BLL.UnitTests;

public class OrderServiceTests
{
    private Mock<IOrderRepository> _orderRepositoryMock;
    private Mock<IMapper> _mapperMock;

    [SetUp]
    public void Setup()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _mapperMock = new Mock<IMapper>();
    }

    [Test]
    public async Task AddOrder_Test_Success()
    {
        var orderInsertModelBll = new Models.OrderInsertModel();
        var orderDal = new DAL.Entities.Order();
        _mapperMock.Setup(mapper => mapper.Map<DAL.Entities.Order>(orderInsertModelBll))
                   .Returns(orderDal);
        _orderRepositoryMock.Setup(repository => repository.InsertOrder(orderDal, It.IsAny<CancellationToken>()))
                            .Returns(Task.CompletedTask);

        IOrderService orderService = new OrderService(_orderRepositoryMock.Object, _mapperMock.Object);

        await orderService.AddOrder(orderInsertModelBll);

        _mapperMock.Verify(mapper => mapper.Map<DAL.Entities.Order>(orderInsertModelBll), Times.Once);
        _mapperMock.VerifyNoOtherCalls();

        _orderRepositoryMock.Verify(repository => repository.InsertOrder(orderDal, It.IsAny<CancellationToken>()),
                                    Times.Once);
        _orderRepositoryMock.VerifyNoOtherCalls();
    }

    [Test]
    public void AddOrder_Test_ArgumentNullThrowsException()
    {
        IOrderService orderService = new OrderService(_orderRepositoryMock.Object, _mapperMock.Object);

        async Task Action() => await orderService.AddOrder(null!);

        Assert.That(Action, Throws.TypeOf<ArgumentNullException>());
        _mapperMock.VerifyNoOtherCalls();
        _orderRepositoryMock.VerifyNoOtherCalls();
    }

    [Test]
    public async Task GetOrder_Test_Success()
    {
        const int orderId = 1;
        var orderDal = new DAL.Entities.Order();
        var orderBll = new Models.Order();

        _orderRepositoryMock.Setup(repository => repository.GetOrderById(orderId, It.IsAny<CancellationToken>()))
                            .ReturnsAsync(orderDal);
        _mapperMock.Setup(mapper => mapper.Map<Models.Order>(orderDal)).Returns(orderBll);

        IOrderService orderService = new OrderService(_orderRepositoryMock.Object, _mapperMock.Object);

        var actual = await orderService.GetOrder(orderId);

        Assert.That(actual, Is.EqualTo(orderBll));
        _orderRepositoryMock.Verify(repository => repository.GetOrderById(orderId, It.IsAny<CancellationToken>()),
                                    Times.Once);
        _orderRepositoryMock.VerifyNoOtherCalls();

        _mapperMock.Verify(mapper => mapper.Map<Models.Order>(orderDal), Times.Once);
        _mapperMock.VerifyNoOtherCalls();
    }

    [Test]
    public async Task GetOrders_Test_Success()
    {
        var ordersDal = new List<DAL.Entities.Order> { new() };

        var ordersBll = new List<Models.Order> { new() };

        _orderRepositoryMock.Setup(repository => repository.GetOrders(It.IsAny<CancellationToken>()))
                            .ReturnsAsync(ordersDal);
        _mapperMock.Setup(mapper => mapper.Map<IList<Models.Order>>(ordersDal)).Returns(ordersBll);

        IOrderService orderService = new OrderService(_orderRepositoryMock.Object, _mapperMock.Object);

        var actual = await orderService.GetOrders();

        Assert.That(actual, Is.EquivalentTo(ordersBll));
        _orderRepositoryMock.Verify(repository => repository.GetOrders(It.IsAny<CancellationToken>()),
                                    Times.Once);
        _orderRepositoryMock.VerifyNoOtherCalls();

        _mapperMock.Verify(mapper => mapper.Map<IList<Models.Order>>(ordersDal), Times.Once);
        _mapperMock.VerifyNoOtherCalls();
    }
}
