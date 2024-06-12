//using Microsoft.AspNetCore.Mvc;
//using OsDsII.api.Dtos.ServiceOrders;
//using OsDsII.api.Exceptions;
//using OsDsII.api.Models;
//using OsDsII.api.Repository.CommentsRepository;
//using OsDsII.api.Repository.ServiceOrderRepository;
//using OsDsII.api.Services.Comments;
//using OsDsII.api.Services.ServiceOrders;

//namespace OsDsII.api.Controllers
//{

//    [ApiController]
//    [Route("ServiceOrders/{id}/comment")]
//    public class CommentController : ControllerBase
//    {

//        private ICommentsService _commentsService;
//        private IServiceOrderService _serviceOrderService;

//        public CommentController(ICommentsService commentsService)
//        {
//            _commentsService = commentsService;
//        }

//        [HttpGet]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<IActionResult> GetCommentsAsync(int serviceOrderId)
//        {
//            try
//            {
//                ServiceOrderDto serviceOrderWithComments = await _commentsService.GetServiceOrderWithComments(serviceOrderId);
//                return Ok(serviceOrderWithComments);
//            }
//            catch (BaseException ex)
//            {
//                return ex.GetResponse();
//            }
//        }

//        [HttpPost]
//        public async Task<IActionResult> AddComment(Comment comment, int serviceOrderId)
//        {
//            try
//            {
//                // no proprio _serviceOrderService
//                ServiceOrder os = await _serviceOrderRepository.GetServiceOrderFromUser(serviceOrderId);

//                if (os == null)
//                {
//                    throw new Exception("Service Order not found.");
//                }

//                // serviço de comentários
//                // no método, você retorna ESSE \/ AQUI
//                Comment commentExists = HandleCommentObject(serviceOrderId, comment.Description);

//                await _commentsRepository.AddCommentAsync(commentExists); // This line adds the comment to the context

//                return Ok(commentExists);
//            }
//            catch (BaseException ex)
//            {
//                return ex.GetResponse();
//            }
//        }

//        // vai pro services de comentário
//        private Comment HandleCommentObject(int id, string description)
//        {
//            return new Comment
//            {
//                Description = description,
//                ServiceOrderId = id
//            };
//        }
//    }
//}

