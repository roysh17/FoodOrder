using FoodOrder.Repository.Dtos.Foods;
using FoodOrder.Repository.Dtos.Orders;
using FoodOrder.Repository.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrder.App.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;

        }

        public IActionResult Index(GetOrdersWithPagingFilter filter)
        {
            return View();
        }

        public async Task<IActionResult> Detail(int id)
        {
            var order = await _orderService.GetOrderById(id);
            var orderItems = await _orderService.GetOrderItemsByOrderId(order.OrderNo);

            ViewData["order"] = order;
            ViewData["orderItems"] = orderItems.ToList();

            return View();
        }

        public async Task<IActionResult> GetOrdersInJson(GetOrdersWithPagingFilter filter)
        {
            List<Order> orders = new List<Order>();
            var totalRows = 0;

            try
            {
                orders = (await _orderService.GetOrdersWithPaging(filter)).ToList();
                totalRows = await _orderService.GetOrdersWithPagingRowsCount(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            var returnObj = new
            {
                draw = filter.Draw,
                recordsTotal = totalRows,
                recordsFiltered = totalRows,
                data = orders
            };

            return Json(returnObj);
        }

        public async Task<IActionResult> GetFoodByFoodIdInJson(int foodId)
        {

            Food food = new Food();

            try
            {
                food = await _orderService.GetFoodById(foodId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            var returnObj = new
            {
                data = food
            };

            return Json(returnObj);
        }


        private async Task GetFoods()
        {
            var foods = await _orderService.GetFoods();
            ViewData["foods"] = foods.ToList();
        }

        public async Task<IActionResult> Add()
        {

            await GetFoods();

            var newOrder = new CreateOrderDto();

            return View(newOrder);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem()
        {

            var orderNo = DateTime.Now.ToString("yyMMddHHmmssff");

            var arrItemQty = Request.Form["itemQty[]"];
            var arrItemFoodId = Request.Form["itemFoodId[]"];
            var arrItemPrice = Request.Form["itemPrice[]"];
            var customerName = Request.Form["customerName"];

            var addOrderItemResult = 0;

            var totalAllPrice = 0;

            for (int i = 0; i < arrItemFoodId.Count; i++)
            {
                var foodId = Convert.ToInt32(arrItemFoodId[i]);
                var qty = Convert.ToInt32(arrItemQty[i]);
                var itemPrice = Convert.ToInt32(arrItemPrice[i].ToString());
                var totalPrice = itemPrice * qty;
                totalAllPrice += totalPrice;

                // add order item

                var orderItemToAdd = new CreateOrderItemDto
                {
                    OrderNo = orderNo,
                    FoodId = foodId,
                    Qty = qty,
                    TotalPrice = totalPrice,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now
                };
                addOrderItemResult += await _orderService.AddOrderItem(orderItemToAdd);
            }

            // add order

            var orderToAdd = new CreateOrderDto
            {
                OrderNo = orderNo,
                OrderDate = DateTime.Now,
                CustomerName = customerName,
                TotalPrice = totalAllPrice,
                CreatedBy = "Admin",
                CreatedDate = DateTime.Now
            };
            var orderId = await _orderService.AddOrder(orderToAdd);


            if (orderId > 0 && addOrderItemResult > 0)
            {
                TempData["Message"] = "Pesanan sudah berhasil ditambahkan.";
                return RedirectToAction($"Detail", "Orders", new { id = orderId });
            }

            await GetFoods();

            TempData["ErrMessage"] = "Gagal, terjadi kesalahan pada saat proses data.";
            return View("Add");
        }

        [HttpPost]
        public async Task<IActionResult> updatePaymentStatus(int orderId)
        {
            var updateResult = await _orderService.UpdatePaymentStatus(orderId);

            if (updateResult > 0)
            {
                TempData["Message"] = "Proses pembayaran sudah berhasil.";
                return RedirectToAction("Index", "Orders");
            }


            TempData["ErrorMessage"] = "Gagal, terjadi kesalahan pada saat proses data.";

            var order = await _orderService.GetOrderById(orderId);
            var orderItems = await _orderService.GetOrderItemsByOrderId(order.OrderNo);

            ViewData["order"] = order;
            ViewData["orderItems"] = orderItems.ToList();

            return View("Detail");
        }
    }
}
