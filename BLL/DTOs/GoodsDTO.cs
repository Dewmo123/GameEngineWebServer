using DAL.VOs;

namespace BLL.DTOs
{
    public record class GoodsDTO
    {
        public GoodsType GoodsType { get; set; }
        public int Amount { get; set; }
    }
}
