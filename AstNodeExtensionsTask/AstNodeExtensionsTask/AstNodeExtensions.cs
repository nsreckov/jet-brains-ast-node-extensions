using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstNodeExtensionsTask
{
    public static class AstNodeExtensions
    {
        /// <summary>
        /// Checks if two AST nodes are equivalent
        /// ignoring whitespace/comment tokens.
        /// </summary>
        public static bool IsEquivalentTo(this IAstNode node, IAstNode other)
        {
            if (node == null && other == null)
                return true;
            if (node == null || other == null)
                return false;

            if (node.GetText() != other.GetText())
            {
                return false;
            }
                
            var child1= node.FirstChild;
            var child2= other.FirstChild;

            while (child1 != null && child2 != null)
            {
                child1 = CheckAndSkipWhitespaceAndComments(child1);
                child2 = CheckAndSkipWhitespaceAndComments(child2);

                if (child1 == null && child2 == null)
                {
                    return true;
                }
                if (child1 == null || child2 == null)
                {
                    return false;
                }
                if (!child1.IsEquivalentTo(child2))
                {
                    return false;
                }
                child1 = CheckAndSkipWhitespaceAndComments(child1.NextSibling);
                child2 = CheckAndSkipWhitespaceAndComments(child2.NextSibling);
            }

            return CheckAndSkipWhitespaceAndComments(child1) == null &&
                   CheckAndSkipWhitespaceAndComments(child2) == null;



        }

        public static IAstNode CheckAndSkipWhitespaceAndComments(IAstNode node)
        {
            while (node != null && node.IsWhitespaceOrComment)
                node = node.NextSibling;
            return node;
        }
    }
}
