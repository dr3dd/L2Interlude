using Core.Module.ItemData;
using Core.Module.Player;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Test;

public class PlayerArmorTest : IClassFixture<PlayerInstanceFixture>
{
    private readonly PlayerInstance _playerInstance;
    private readonly ItemDataInit _itemDataInit;
    
    public PlayerArmorTest(PlayerInstanceFixture playerInstanceFixture)
    {
        _playerInstance = playerInstanceFixture.GetPlayerInstance();
        _itemDataInit = _playerInstance.ServiceProvider.GetRequiredService<ItemDataInit>();
    }
    
    /// <summary>
    /// Helmet
    /// </summary>
    [Fact]
    public void AddHardLeatherHelmetTest()
    {
        var item = _itemDataInit.GetItemByName("hard_leather_helmet");
        var itemInstance = new ItemInstance(0, _playerInstance.ServiceProvider)
        {
            ItemId = item.ItemId,
            ItemData = item,
            UserItemId = item.ItemId
        };
        _playerInstance.PlayerInventory().EquipItemInBodySlot(itemInstance);
        _playerInstance.PlayerInventory().AddItemToInventoryCollection(itemInstance);
        var pDef = _playerInstance.CharacterCombat().GetPhysicalDefence();
        Assert.Equal(88, pDef);
    }
    
    /// <summary>
    /// Body - Upper
    /// </summary>
    [Fact]
    public void AddWoodenBreastplateTest()
    {
        var item = _itemDataInit.GetItemByName("wooden_breastplate");
        var itemInstance = new ItemInstance(1, _playerInstance.ServiceProvider)
        {
            ItemId = item.ItemId,
            ItemData = item,
            UserItemId = item.ItemId
        };
        _playerInstance.PlayerInventory().EquipItemInBodySlot(itemInstance);
        _playerInstance.PlayerInventory().AddItemToInventoryCollection(itemInstance);
        var pDef = _playerInstance.CharacterCombat().GetPhysicalDefence();
        Assert.Equal(90, pDef);
    }
    
    /// <summary>
    /// Body - Lower
    /// </summary>
    [Fact]
    public void AddWoodenGaitersTest()
    {
        var item = _itemDataInit.GetItemByName("wooden_gaiters");
        var itemInstance = new ItemInstance(2, _playerInstance.ServiceProvider)
        {
            ItemId = item.ItemId,
            ItemData = item,
            UserItemId = item.ItemId
        };
        _playerInstance.PlayerInventory().EquipItemInBodySlot(itemInstance);
        _playerInstance.PlayerInventory().AddItemToInventoryCollection(itemInstance);
        var pDef = _playerInstance.CharacterCombat().GetPhysicalDefence();
        Assert.Equal(85, pDef);
    }
    
    /// <summary>
    /// Gloves
    /// </summary>
    [Fact]
    public void AddBracerTest()
    {
        var item = _itemDataInit.GetItemByName("bracer");
        var itemInstance = new ItemInstance(3, _playerInstance.ServiceProvider)
        {
            ItemId = item.ItemId,
            ItemData = item,
            UserItemId = item.ItemId
        };
        _playerInstance.PlayerInventory().EquipItemInBodySlot(itemInstance);
        _playerInstance.PlayerInventory().AddItemToInventoryCollection(itemInstance);
        var pDef = _playerInstance.CharacterCombat().GetPhysicalDefence();
        Assert.Equal(83, pDef);
    }
    
    /// <summary>
    /// Boots
    /// </summary>
    [Fact]
    public void AddBootsTest()
    {
        var item = _itemDataInit.GetItemByName("boots");
        var itemInstance = new ItemInstance(4, _playerInstance.ServiceProvider)
        {
            ItemId = item.ItemId,
            ItemData = item,
            UserItemId = item.ItemId
        };
        _playerInstance.PlayerInventory().EquipItemInBodySlot(itemInstance);
        _playerInstance.PlayerInventory().AddItemToInventoryCollection(itemInstance);
        var pDef = _playerInstance.CharacterCombat().GetPhysicalDefence();
        Assert.Equal(84, pDef);
    }
    
    /// <summary>
    /// Underwear
    /// </summary>
    [Fact]
    public void AddWoolUnderwearTest()
    {
        var item = _itemDataInit.GetItemByName("wool_underwear_set");
        var itemInstance = new ItemInstance(5, _playerInstance.ServiceProvider)
        {
            ItemId = item.ItemId,
            ItemData = item,
            UserItemId = item.ItemId
        };
        _playerInstance.PlayerInventory().EquipItemInBodySlot(itemInstance);
        _playerInstance.PlayerInventory().AddItemToInventoryCollection(itemInstance);
        var pDef = _playerInstance.CharacterCombat().GetPhysicalDefence();
        Assert.Equal(77, pDef);
    }
    
    /// <summary>
    /// Necklace
    /// </summary>
    [Fact]
    public void AddBlueDiamondNecklaceTest()
    {
        var item = _itemDataInit.GetItemByName("blue_diamond_necklace");
        var itemInstance = new ItemInstance(6, _playerInstance.ServiceProvider)
        {
            ItemId = item.ItemId,
            ItemData = item,
            UserItemId = item.ItemId
        };
        _playerInstance.PlayerInventory().EquipItemInBodySlot(itemInstance);
        _playerInstance.PlayerInventory().AddItemToInventoryCollection(itemInstance);
        var mDef = _playerInstance.CharacterCombat().GetMagicalDefence();
        Assert.Equal(64, mDef);
    }
    
    /// <summary>
    /// Earning
    /// </summary>
    [Fact]
    public void AddCoralEarningTest()
    {
        var item = _itemDataInit.GetItemByName("coral_earing");
        var itemInstanceLeftEarning = new ItemInstance(7, _playerInstance.ServiceProvider)
        {
            ItemId = item.ItemId,
            ItemData = item,
            UserItemId = 1
        };
        var itemInstanceRightEarning = new ItemInstance(8, _playerInstance.ServiceProvider)
        {
            ItemId = item.ItemId,
            ItemData = item,
            UserItemId = 2
        };
        _playerInstance.PlayerInventory().EquipItemInBodySlot(itemInstanceLeftEarning);
        _playerInstance.PlayerInventory().AddItemToInventoryCollection(itemInstanceLeftEarning);
        _playerInstance.PlayerInventory().EquipItemInBodySlot(itemInstanceRightEarning);
        _playerInstance.PlayerInventory().AddItemToInventoryCollection(itemInstanceRightEarning);
        var mDef = _playerInstance.CharacterCombat().GetMagicalDefence();
        Assert.Equal(74, mDef);
    }
    
    /// <summary>
    /// Ring
    /// </summary>
    [Fact]
    public void AddBlueCoralRingTest()
    {
        var item = _itemDataInit.GetItemByName("blue_coral_ring");
        var itemInstanceRightRing = new ItemInstance(9, _playerInstance.ServiceProvider)
        {
            ItemId = item.ItemId,
            ItemData = item,
            UserItemId = 1
        };
        var itemInstanceLeftRing = new ItemInstance(10, _playerInstance.ServiceProvider)
        {
            ItemId = item.ItemId,
            ItemData = item,
            UserItemId = 2
        };
        _playerInstance.PlayerInventory().EquipItemInBodySlot(itemInstanceRightRing);
        _playerInstance.PlayerInventory().AddItemToInventoryCollection(itemInstanceRightRing);
        _playerInstance.PlayerInventory().EquipItemInBodySlot(itemInstanceLeftRing);
        _playerInstance.PlayerInventory().AddItemToInventoryCollection(itemInstanceLeftRing);
        var mDef = _playerInstance.CharacterCombat().GetMagicalDefence();
        Assert.Equal(67, mDef);
    }
}