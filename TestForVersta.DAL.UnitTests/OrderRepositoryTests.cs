using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TestForVersta.DAL.Entities;
using TestForVersta.DAL.Repositories;

namespace TestForVersta.DAL.UnitTests;

public class OrderRepositoryTests
{
    private ApplicationContext _applicationContext;
    private IEnumerable<Order> _orders;

    private static IEnumerable<Order> GetOrders(int count)
    {
        var random = new Random();
        for (var i = 1; i <= count; i++)
        {
            yield return new Order
            {
                SenderCity = $"SC{i}",
                SenderAddress = $"SA{i}",
                ReceiverCity = $"RC{i}",
                ReceiverAddress = $"RA{i}",
                Weight = random.NextDouble() * 10,
                DeliveryDate = new DateTime(2021, random.Next(1, 12), random.Next(1, 28))
            };
        }
    }

    [SetUp]
    public async Task Setup()
    {
        var dbContextOptions = new DbContextOptionsBuilder().UseInMemoryDatabase("TestDb").Options;
        _applicationContext = new ApplicationContext(dbContextOptions);

        await _applicationContext.Database.EnsureCreatedAsync();

        var orders = GetOrders(5).ToList();
        await _applicationContext.Orders.AddRangeAsync(orders);
        await _applicationContext.SaveChangesAsync();
        _orders = orders.Select((order, i) => order with { Id = i + 1 });
    }

    [TearDown]
    public async Task TearDown()
    {
        await _applicationContext.Database.EnsureDeletedAsync();
        await _applicationContext.DisposeAsync();
    }

    [Test]
    public async Task GetOrders_Test_Success()
    {
        var orderRepository = new OrderRepository(_applicationContext);
        var initialList = await _applicationContext.Orders.ToListAsync();

        var actual = await orderRepository.GetOrders();

        var finalList = await _applicationContext.Orders.ToListAsync();
        Assert.That(actual, Is.EquivalentTo(initialList).And.EquivalentTo(finalList));
    }

    [Test]
    public async Task GetOrderById_Test_Success()
    {
        var expected = _orders.First(order => order.Id == 3);
        var orderRepository = new OrderRepository(_applicationContext);
        var initialList = await _applicationContext.Orders.ToListAsync();

        var actual = await orderRepository.GetOrderById(3);

        Assert.That(actual, Is.EqualTo(expected));

        var finalList = await _applicationContext.Orders.ToListAsync();
        Assert.That(finalList, Is.EquivalentTo(initialList));
    }

    [Test]
    public async Task Insert_Test_Success()
    {
        var orderToInsert = new Order
        {
            SenderCity = $"SC_New",
            SenderAddress = $"SA_New",
            ReceiverCity = $"RC_New",
            ReceiverAddress = $"RA_New",
            Weight = 10.13,
            DeliveryDate = new DateTime(2021, 12, 12)
        };
        var orderRepository = new OrderRepository(_applicationContext);
        var initialList = await _applicationContext.Orders.ToListAsync();
        var expected = initialList.Append(orderToInsert with
        {
            Id = await _applicationContext.Orders.MaxAsync(order => order.Id) + 1
        });

        await orderRepository.InsertOrder(orderToInsert);

        var finalList = await _applicationContext.Orders.ToListAsync();
        Assert.That(expected, Is.EquivalentTo(finalList));
    }
}
