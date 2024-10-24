namespace SpaceCorps.Tests.Game.Player;

public class UnitTests
{
    [Fact]
    public void Player_Position_IsSet()
    {
        var player = new Business.EntityLogic.Player();
    
        Assert.NotNull(player.Position);
    }
    
    [Fact]
    public void Player_IsEntity()
    {
        var player = new Business.EntityLogic.Player();
    
        Assert.IsAssignableFrom<Business.EntityLogic.Entity>(player);
    }
    
    [Fact]
    public void Player_Position_IsSetToZero()
    {
        var player = new Business.EntityLogic.Player();

        var playerPos = player.Position.Get();
        
        Assert.Equal(0, playerPos.X);
        Assert.Equal(0, playerPos.Y);
        Assert.Equal(0, playerPos.Z);
    }
    
    [Fact]
    public void Position_IsSet()
    {
        var player = new Business.EntityLogic.Player();
        var newPosition = new Business.EntityLogic.Position(1, 2, 3);

        player.Position.Set(newPosition);
        var playerPos = player.Position.Get();
        
        Assert.Equal(1, playerPos.X);
        Assert.Equal(2, playerPos.Y);
        Assert.Equal(3, playerPos.Z);
    }
    
}