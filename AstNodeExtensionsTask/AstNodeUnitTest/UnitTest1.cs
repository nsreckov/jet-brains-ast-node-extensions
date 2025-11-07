using AstNodeExtensionsTask;

namespace AstNodeUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1_SimpleLeafNodes()
        {
            var a = new TestNode { Text = "x" };
            var b = new TestNode { Text = "x" };

            Assert.True(a.IsEquivalentTo(b));
        }

        [Fact]
        public void Test2_DifferentText()
        {
            var a = new TestNode { Text = "x" };
            var b = new TestNode { Text = "y" };

            Assert.False(a.IsEquivalentTo(b));
        }

        [Fact]
        public void Test3_WithChildren()
        {
            var a = new TestNode
            {
                Text = "root",
                FirstChild = new TestNode { Text = "child1" }
            };

            var b = new TestNode
            {
                Text = "root",
                FirstChild = new TestNode { Text = "child1" }
            };

            Assert.True(a.IsEquivalentTo(b));
        }

        [Fact]
        public void Test4_IgnoresWhitespace()
        {
            var whitespace = new TestNode
            {
                Text = "  ",
                IsWhitespaceOrComment = true
            };

            var child1 = new TestNode { Text = "child" };
            child1.NextSibling = whitespace;
            whitespace.PrevSibling = child1;

            var a = new TestNode
            {
                Text = "root",
                FirstChild = child1
            };

            var b = new TestNode
            {
                Text = "root",
                FirstChild = new TestNode { Text = "child" }
            };

            Assert.True(a.IsEquivalentTo(b));
        }

        [Fact]
        public void Test5_DifferentChildCount()
        {
            var child1 = new TestNode { Text = "child1" };
            var child2 = new TestNode { Text = "child2" };
            child1.NextSibling = child2;
            child2.PrevSibling = child1;

            var a = new TestNode
            {
                Text = "root",
                FirstChild = child1
            };

            var b = new TestNode
            {
                Text = "root",
                FirstChild = new TestNode { Text = "child1" }
            };

            Assert.False(a.IsEquivalentTo(b));
        }

        [Fact]
        public void Test6_BothNull()
        {
            Assert.True(AstNodeExtensions.IsEquivalentTo(null, null));
        }

        [Fact]
        public void Test7_OneNull()
        {
            var a = new TestNode { Text = "x" };
            Assert.False(a.IsEquivalentTo(null));
            Assert.False(AstNodeExtensions.IsEquivalentTo(null, a));
        }

        [Fact]
        public void Test8_MultipleSiblings()
        {
            var a_child1 = new TestNode { Text = "child1" };
            var a_child2 = new TestNode { Text = "child2" };
            var a_child3 = new TestNode { Text = "child3" };
            a_child1.NextSibling = a_child2;
            a_child2.PrevSibling = a_child1;
            a_child2.NextSibling = a_child3;
            a_child3.PrevSibling = a_child2;

            var a = new TestNode { Text = "root", FirstChild = a_child1 };

            var b_child1 = new TestNode { Text = "child1" };
            var b_child2 = new TestNode { Text = "child2" };
            var b_child3 = new TestNode { Text = "child3" };
            b_child1.NextSibling = b_child2;
            b_child2.PrevSibling = b_child1;
            b_child2.NextSibling = b_child3;
            b_child3.PrevSibling = b_child2;

            var b = new TestNode { Text = "root", FirstChild = b_child1 };

            Assert.True(a.IsEquivalentTo(b));
        }

        [Fact]
        public void Test9_WhitespaceAtStart()
        {
            var whitespace = new TestNode { Text = "\n", IsWhitespaceOrComment = true };
            var child = new TestNode { Text = "child1" };
            whitespace.NextSibling = child;
            child.PrevSibling = whitespace;

            var a = new TestNode { Text = "root", FirstChild = whitespace };

            var b = new TestNode { Text = "root", FirstChild = new TestNode { Text = "child1" } };

            Assert.True(a.IsEquivalentTo(b));
        }

        [Fact]
        public void Test10_MultipleWhitespaceNodes()
        {
            var ws1 = new TestNode { Text = " ", IsWhitespaceOrComment = true };
            var ws2 = new TestNode { Text = "\t", IsWhitespaceOrComment = true };
            var child = new TestNode { Text = "child" };
            var ws3 = new TestNode { Text = "\n", IsWhitespaceOrComment = true };
            var ws4 = new TestNode { Text = "  ", IsWhitespaceOrComment = true };

            ws1.NextSibling = ws2;
            ws2.PrevSibling = ws1;
            ws2.NextSibling = child;
            child.PrevSibling = ws2;
            child.NextSibling = ws3;
            ws3.PrevSibling = child;
            ws3.NextSibling = ws4;
            ws4.PrevSibling = ws3;

            var a = new TestNode { Text = "root", FirstChild = ws1 };

            var b = new TestNode { Text = "root", FirstChild = new TestNode { Text = "child" } };

            Assert.True(a.IsEquivalentTo(b));
        }

        [Fact]
        public void Test11_CommentsIgnored()
        {
            var a_child1 = new TestNode { Text = "child1" };
            var comment = new TestNode { Text = "/* comment */", IsWhitespaceOrComment = true };
            var a_child2 = new TestNode { Text = "child2" };
            a_child1.NextSibling = comment;
            comment.PrevSibling = a_child1;
            comment.NextSibling = a_child2;
            a_child2.PrevSibling = comment;

            var a = new TestNode { Text = "root", FirstChild = a_child1 };

            var b_child1 = new TestNode { Text = "child1" };
            var b_child2 = new TestNode { Text = "child2" };
            b_child1.NextSibling = b_child2;
            b_child2.PrevSibling = b_child1;

            var b = new TestNode { Text = "root", FirstChild = b_child1 };

            Assert.True(a.IsEquivalentTo(b));
        }

        [Fact]
        public void Test12_NestedChildren()
        {
            var grandchild1 = new TestNode { Text = "grandchild1" };
            var child1 = new TestNode { Text = "child1", FirstChild = grandchild1 };
            var a = new TestNode { Text = "root", FirstChild = child1 };

            var b_grandchild1 = new TestNode { Text = "grandchild1" };
            var b_child1 = new TestNode { Text = "child1", FirstChild = b_grandchild1 };
            var b = new TestNode { Text = "root", FirstChild = b_child1 };

            Assert.True(a.IsEquivalentTo(b));
        }

        [Fact]
        public void Test13_DifferentNestedChildren()
        {
            var grandchild1 = new TestNode { Text = "grandchild1" };
            var child1 = new TestNode { Text = "child1", FirstChild = grandchild1 };
            var a = new TestNode { Text = "root", FirstChild = child1 };

            var b_grandchild2 = new TestNode { Text = "grandchild2" };
            var b_child1 = new TestNode { Text = "child1", FirstChild = b_grandchild2 };
            var b = new TestNode { Text = "root", FirstChild = b_child1 };

            Assert.False(a.IsEquivalentTo(b));
        }

        [Fact]
        public void Test14_AllWhitespace()
        {
            // Tree A: root -> ws1 -> ws2
            var ws1 = new TestNode { Text = " ", IsWhitespaceOrComment = true };
            var ws2 = new TestNode { Text = "\n", IsWhitespaceOrComment = true };
            ws1.NextSibling = ws2;
            ws2.PrevSibling = ws1;

            var a = new TestNode { Text = "root", FirstChild = ws1 };

            var b = new TestNode { Text = "root" };

            Assert.True(a.IsEquivalentTo(b));
        }

        [Fact]
        public void Test15_EmptyTextNodes()
        {
            var a = new TestNode { Text = "" };
            var b = new TestNode { Text = "" };

            Assert.True(a.IsEquivalentTo(b));
        }
    }
}