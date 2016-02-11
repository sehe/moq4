namespace Moq.Tests.Linq
{
	using Xunit;

	public class BehaviorFixture
	{
		[Fact]
		public void NoQuery_Default()
		{
			var target = Mock.Of<IFoo>();
			Assert.Equal(MockBehavior.Default, Mock.Get(target).Behavior);
		}

		[Fact]
		public void WithQuery_Default()
		{
			var target = Mock.Of<IFoo>(x => x.BoolProperty1);
			Assert.Equal(MockBehavior.Default, Mock.Get(target).Behavior);
		}

		[Fact]
		public void NoQuery_Strict()
		{
			var target = Mock.Of<IFoo>(MockBehavior.Strict);
			Assert.Equal(MockBehavior.Strict, Mock.Get(target).Behavior);
		}

		[Fact]
		public void NoQuery_Loose()
		{
			var target = Mock.Of<IFoo>(MockBehavior.Loose);
			Assert.Equal(MockBehavior.Loose, Mock.Get(target).Behavior);
		}

		[Fact]
		public void WithQuery_Strict()
		{
			var target = Mock.Of<IFoo>(x => x.BoolProperty1, MockBehavior.Strict);
			Assert.Equal(MockBehavior.Strict, Mock.Get(target).Behavior);
		}

		[Fact]
		public void WithQuery_Loose()
		{
			var target = Mock.Of<IFoo>(x => x.BoolProperty1, MockBehavior.Loose);
			Assert.Equal(MockBehavior.Loose, Mock.Get(target).Behavior);
		}

		[Fact]
		public void WithQuery_ThrowsWhenStrict()
		{
			var target = Mock.Of<IFoo>(x => x.BoolProperty1, MockBehavior.Strict);
			Assert.DoesNotThrow(() => { var temp = target.BoolProperty1; });
			Assert.Throws<MockException>(() => { var temp = target.BoolProperty2; });
			Assert.Throws<MockException>(() => target.BoolMethod());
		}
		
		[Fact]
		public void NoQuery_ThrowsWhenStrict()
		{
			var target = Mock.Of<IFoo>(MockBehavior.Strict);
			Assert.Throws<MockException>(() => target.BoolProperty1);
			Assert.Throws<MockException>(() => target.BoolProperty2);
			Assert.Throws<MockException>(() => target.BoolMethod());
		}

		[Fact]
		public void WithQuery_DoesNotThrowsWhenLoose()
		{
			var target = Mock.Of<IFoo>(x => x.BoolProperty1, MockBehavior.Loose);
			Assert.DoesNotThrow(() => { var temp = target.BoolProperty1; });
			Assert.True(target.BoolProperty1);
			
			Assert.DoesNotThrow(() => { var temp = target.BoolProperty2; });
			Assert.False(target.BoolProperty2);

			Assert.DoesNotThrow(() => target.BoolMethod());
			Assert.False(target.BoolMethod());

			Assert.DoesNotThrow(() => target.VoidMethod());

		}

		[Fact]
		public void NoQuery_DoesNotThrowsWhenLoose()
		{
			var target = Mock.Of<IFoo>(MockBehavior.Loose);
			Assert.DoesNotThrow(() => { var temp = target.BoolProperty1; });
			Assert.DoesNotThrow(() => { var temp = target.BoolProperty2; });
			Assert.DoesNotThrow(() => target.BoolMethod());
			Assert.DoesNotThrow(() => target.VoidMethod());
		}

		public interface IFoo
		{
			bool BoolProperty1 { get; set; }
			bool BoolProperty2 { get; set; }
			bool BoolMethod();
			void VoidMethod();
		}
	}
}
