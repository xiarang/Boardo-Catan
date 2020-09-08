using System;

namespace Model
{
    public enum Sellable
    {
        Road,
        City,
        DevelopmentCard,
        House
    }

    public static class SellableUtils
    {
        public static Resource[] GetCost(this Sellable sellable)
        {
             switch (sellable)
            {
                case Sellable.Road:
                    return new[] {Resource.Brick, Resource.Wood};
                    break;
                case Sellable.City:
                    return new[] {Resource.Stone, Resource.Stone, Resource.Stone, Resource.Wheat, Resource.Wheat};
                    break;
                case Sellable.DevelopmentCard:
                    return new[] {Resource.Stone, Resource.Sheep, Resource.Wheat};
                    break;
                case Sellable.House:
                    return new[] {Resource.Brick, Resource.Sheep, Resource.Wheat, Resource.Wood};
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sellable), sellable, null);
            }
        } 
    }
}