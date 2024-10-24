namespace SpaceCorps.Tests.Game.Entity
{
    public class UnitTests
    {
        [Fact]
        public void Entity_Position_IsSet()
        {
            var entity = new Business.EntityLogic.Entity();
        
            Assert.NotNull(entity.Position);
        }
    
        [Fact]
        public void Player_IsEntity()
        {
            var player = new Business.EntityLogic.Player();
        
            Assert.IsAssignableFrom<Business.EntityLogic.Entity>(player);
        }
    
        [Fact]
        public void Entity_Position_IsSetToZero()
        {
            var entity = new Business.EntityLogic.Entity();

            var entityPos = entity.Position.Get();
            
            Assert.Equal(0, entityPos.X);
            Assert.Equal(0, entityPos.Y);
            Assert.Equal(0, entityPos.Z);
        }
    
        [Fact]
        public void Position_IsSet()
        {
            var entity = new Business.EntityLogic.Entity();
            var newPosition = new Business.EntityLogic.Position(1, 2, 3);

            entity.Position.Set(newPosition);
            var entityPos = entity.Position.Get();
            
            Assert.Equal(1, entityPos.X);
            Assert.Equal(2, entityPos.Y);
            Assert.Equal(3, entityPos.Z);
        }
    }
}