using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using PruebaTuya.Domain.Interfaces;
using PruebaTuya.Application.Services;
using PruebaTuya.Domain.Entities;
using Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Castle.Core.Resource;
using Xunit.Abstractions;

namespace PruebaTuya.Test
{
    public class OrdenServiceTests
    {
        private readonly Mock<IOrderRepository> _repositoryMock;
        private readonly Mock<ICustomerRepository> _repositoryCustomerMock;
        private readonly OrderService _orderService;
        private readonly Mock<IOrderService> _orderServiceMock = new();

        public OrdenServiceTests()
        {
            _repositoryMock = new Mock<IOrderRepository>();
            _repositoryCustomerMock = new Mock<ICustomerRepository>();
            _orderService = new OrderService(_repositoryMock.Object);
        }
        private OrdersController CreateController() =>
       new OrdersController(_orderServiceMock.Object, _repositoryCustomerMock.Object, _repositoryMock.Object);

        [Fact]
        public void CrearPedido_ClienteNoExiste_RetornaNotFound()
        {
            // Arrange
            var order = new Order(1) { CustomerId = 1, Items = new List<OrderItem>(), Status = "Nuevo" };
            _repositoryCustomerMock.Setup(r => r.GetById(1)).Returns((Customer)null);
            var controller = CreateController();

            // Act
            var result = controller.CrearPedido(order);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Cliente no encontrado", notFoundResult.Value);
        }

        [Fact]
        public void CrearPedido_ClienteExiste_RetornaOkConOrden()
        {
            // Arrange
            var customer = new Customer("sandra Montejo", "sandra_965_@hotmail.com");
            var items = new List<OrderItem> { new OrderItem("cargador",2,200),
            new OrderItem("bateria",5,1000)};
            var newOrder = new Order(1);//123,1,items,"Nuevo"
            newOrder.AddItem(new OrderItem("cargador", 2, 200));
            newOrder.AddItem(new OrderItem("bateria", 5, 1000));
            customer.Id= newOrder.CustomerId;

            var expectedOrder = new Order(1)
            {
                CustomerId = 1,
                Status = "Pendiente",
                Items = newOrder.Items,
                OrderDate = DateTime.UtcNow
            };

            
            
            _orderServiceMock.Setup(s => s.CreateOrder(customer, items, "Nuevo"))
                       .Returns(expectedOrder);

            
            var controller = new OrdersController(_orderServiceMock.Object, _repositoryCustomerMock.Object, _repositoryMock.Object);


            // Act
            var result = controller.CrearPedido(newOrder);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Verifica que sea un 200 OK
            var returnValue = Assert.IsType<Order>(okResult.Value); // Verifica que el contenido sea Order
            Assert.Equal(expectedOrder.CustomerId, returnValue.CustomerId);
            Assert.Equal(expectedOrder.Status, returnValue.Status);
        }

        [Fact]
        public async Task Cancelar_OrdenNoCancelada_RetornaBadRequest()
        {
            // Arrange
            _orderServiceMock.Setup(s => s.CancelOrder(10)).Returns(false);
            var controller = CreateController();

            // Act
            var result = await controller.Cancelar(10);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("No se puede cancelar la orden.", badRequest.Value);
        }
        [Fact]
        public async Task Cancelar_OrdenCancelada_RetornaOk()
        {
            // Arrange
            _orderServiceMock.Setup(s => s.CancelOrder(5)).Returns(true);
            var controller = CreateController();

            // Act
            var result = await controller.Cancelar(5);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Orden cancelada exitosamente.", okResult.Value);
        }
    }
}
