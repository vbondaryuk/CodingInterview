using System.Runtime.CompilerServices;

namespace CodingInterview.Coding.Tasks
{
    public class RemoveNthFrom
    {
        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            ListNode dummy = new ListNode(0) { next = head };

            ListNode first = dummy;
            ListNode second = dummy;
            
            // n = 2
            // Scroll FIRST two times  
            for (int i = 1; i <= n + 1; i++)
            {
                first = first.next;
            }

            // since we scrolled FIRST n times, we exactly know that the SECOND stopped
            // before n.
            while (first != null)
            {
                first = first.next;
                second = second.next;
            }

            // remove Nth node from linked list
            second.next = second.next.next;

            return dummy.next;
        }
    }
}