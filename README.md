# AST Node Extensions

A C# library providing extension methods for working with Abstract Syntax Tree (AST) nodes, with functionality to compare AST structures while ignoring whitespace and comment tokens.

## Overview

This project implements extension methods for the `IAstNode` interface, enabling semantic comparison of AST nodes. The primary functionality allows you to determine if two AST nodes are structurally equivalent, automatically skipping whitespace and comment nodes during comparison.

## Features

- **Semantic Equivalence Check**: Compare two AST nodes for structural equivalence
- **Whitespace & Comment Filtering**: Automatically ignores whitespace and comment tokens during comparison
- **Recursive Tree Traversal**: Deep comparison of entire AST subtrees
- **Null-Safe Operations**: Handles null nodes gracefully

## Project Structure

```
AstNodeExtensionsTask/
├── AstNodeExtensionsTask/          # Main library project
│   ├── IAstNode.cs                 # Core AST node interface
│   ├── AstNodeExtensions.cs        # Extension methods
│   ├── TestNode.cs                 # Test implementation of IAstNode
│   └── Program.cs                  # Entry point (empty)
└── AstNodeUnitTest/                # Unit test project
    └── UnitTest1.cs                # Comprehensive test suite
```

## IAstNode Interface

```csharp
public interface IAstNode
{
    IAstNode Parent { get; }
    IAstNode FirstChild { get; }
    IAstNode NextSibling { get; }
    IAstNode PrevSibling { get; }
    bool IsWhitespaceOrComment { get; }
    string GetText();
}
```

## Extension Methods

### IsEquivalentTo

Checks if two AST nodes are structurally equivalent, ignoring whitespace and comment tokens.

```csharp
public static bool IsEquivalentTo(this IAstNode node, IAstNode other)
```

**Parameters:**
- `node`: The first AST node to compare
- `other`: The second AST node to compare

**Returns:**
- `true` if the nodes are structurally equivalent (ignoring whitespace/comments)
- `false` otherwise

**Example:**
```csharp
var node1 = new TestNode { Text = "x" };
var node2 = new TestNode { Text = "x" };

bool areEqual = node1.IsEquivalentTo(node2); // true
```

### CheckAndSkipWhitespaceAndComments

Helper method that skips over consecutive whitespace and comment nodes.

```csharp
public static IAstNode CheckAndSkipWhitespaceAndComments(IAstNode node)
```

## Requirements

- **.NET 8.0** or higher
- **xUnit** (for running unit tests)

## Building the Project

### Using Visual Studio
1. Open `AstNodeExtensionsTask.sln`
2. Build the solution (Ctrl+Shift+B)

### Using .NET CLI
```bash
cd AstNodeExtensionsTask
dotnet build
```

## Running Tests

### Using Visual Studio
1. Open Test Explorer (Test → Test Explorer)
2. Run All Tests

### Using .NET CLI
```bash
cd AstNodeExtensionsTask
dotnet test
```

## Usage Example

```csharp
using AstNodeExtensionsTask;

// Create AST nodes
var node1 = new TestNode
{
    Text = "root",
    FirstChild = new TestNode { Text = "child1" }
};

var node2 = new TestNode
{
    Text = "root",
    FirstChild = new TestNode 
    { 
        Text = "  ", // whitespace
        IsWhitespaceOrComment = true,
        NextSibling = new TestNode { Text = "child1" }
    }
};

// Compare nodes (whitespace is ignored)
bool equivalent = node1.IsEquivalentTo(node2); // true
```

## Test Coverage

The test suite includes comprehensive test cases for:
- Simple leaf node comparison
- Different text content detection
- Parent-child node structures
- Whitespace and comment filtering
- Multiple siblings
- Complex nested structures
- Edge cases (null nodes, empty trees)

## License

This project is part of a coding exercise/task and is provided as-is for educational purposes.

## Author

nsreckov

## Contributing

This is a task/exercise project. For issues or suggestions, please open an issue in the repository.
