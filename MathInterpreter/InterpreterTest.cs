using Xunit;

namespace MathInterpreter
{
    public class InterpreterTest
    {

        public Interpreter Interpreter = new Interpreter();
        
        [Fact]
        public void TestSingleNumber()
        {
            Assert.Equal(-1.1, Interpreter.Evaluate("-1.1"));
        }
        
        [Fact]
        public void TestAdding()
        {
            Assert.Equal(4, Interpreter.Evaluate("2 + 2"));
        }

        [Fact]
        public void TestSubstracting()
        {
            Assert.Equal(4, Interpreter.Evaluate("6 - 2"));
        }

        [Fact]
        public void TestMultiplying()
        {
            Assert.Equal(4, Interpreter.Evaluate("2 * 2"));
        }

        [Fact]
        public void TestDividing()
        {
            Assert.Equal(4, Interpreter.Evaluate("10 / 2.5"));
        }

        [Fact]
        public void TestPower()
        {
            Assert.Equal(4, Interpreter.Evaluate("2 ^ 2"));
        }

        [Fact]
        public void TestPrecedence()
        {
            Assert.Equal(5.5, Interpreter.Evaluate("2 + 2 * 2 - 2 / 2 ^ 2"));
        }

        [Fact]
        public void TestBrackets()
        {
            Assert.Equal(8, Interpreter.Evaluate("( 2 + 2 ) * 2"));
        }
        
        [Fact]
        public void TestParsing()
        {
            Assert.Equal(6, Interpreter.Evaluate("  2+2 -    1 +     3        "));
        }
    }
}