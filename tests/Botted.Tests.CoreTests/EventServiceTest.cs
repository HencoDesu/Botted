using Botted.Core.Events;
using Botted.Core.Events.Abstractions.Events;
using FluentAssertions;
using Xunit;

namespace Botted.Tests.CoreTests
{
	public class EventServiceTest
	{
		class TestEvent : EventWithData<int> { }

		[Fact]
		public void EventCreatingTest()
		{
			var eventService = new EventService();
			
			var firstEvent = eventService.GetEvent<TestEvent>();
			var secondEvent = eventService.GetEvent<TestEvent>();

			secondEvent.Should().Be(firstEvent);
		}
		
		[Fact]
		public void SubscribeUnsubscribeTest()
		{
			var eventService = new EventService();
			var testEvent = eventService.GetEvent<TestEvent>();
			var invocations = 0;
			
			var subscription = testEvent.Subscribe(() => invocations++);
			testEvent.Raise();
			invocations.Should().Be(1);
			
			subscription.Dispose();
			testEvent.Raise();
			invocations.Should().Be(1);
		}

		[Fact]
		public void SubscribeUnsubscribeWithDataTest()
		{
			var eventService = new EventService();
			var testEvent = eventService.GetEvent<TestEvent>();
			var invocations = 0;
			
			var subscription = testEvent.Subscribe(TestHandler);
			testEvent.Raise(invocations);
			invocations.Should().Be(1);
			
			subscription.Dispose();
			testEvent.Raise(invocations);
			invocations.Should().Be(1);

			void TestHandler(int data)
			{
				data.Should().Be(invocations);
				invocations++;
			}
		}
	}
}