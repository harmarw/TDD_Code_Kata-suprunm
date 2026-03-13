using Xunit;
using StringCalulator;

namespace TestProject1;

public class Tests
{
    [Fact]
    public void Calculate_EmptyString_ReturnsZero()
    {
        // Arrange
        var calculator = new StringCalculator();
        
        //Act
        int result = calculator.Calculate("");
        
        //Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void Calculate_SingleNumber_ReturnsValue()
    {
        // Arrange
        var calculator = new StringCalculator();
        
        //Act
        int result1 = calculator.Calculate("3");
        int result2 = calculator.Calculate("6");
        int result3 = calculator.Calculate("9");
        
        //Assert
        Assert.Equal(3, result1);
        Assert.Equal(6, result2);
        Assert.Equal(9, result3);
    }

    [Fact]
    public void Calculate_TwoNumbersWithComma_ReturnSum()
    {
        //Arrange
        var calculator = new StringCalculator();
        
        //Act
        int result1 = calculator.Calculate("3,5");
        int result2 = calculator.Calculate("5,6");
        int result3 = calculator.Calculate("9,8");
        
        //Assert
        Assert.Equal(8, result1);
        Assert.Equal(11, result2);
        Assert.Equal(17, result3);
    }
    
    [Fact]
    public void Calculate_TwoNumbersWithNewLine_ReturnSum()
    {
        //Arrange
        var calculator = new StringCalculator();
        
        //Act
        int result1 = calculator.Calculate("3\n5");
        int result2 = calculator.Calculate("5\n6");
        int result3 = calculator.Calculate("9\n8");
        
        //Assert
        Assert.Equal(8, result1);
        Assert.Equal(11, result2);
        Assert.Equal(17, result3);
    }
    
    [Fact]
    public void Calculate_ThreeNumbersDelimitedEitherWay_ReturnSum()
    {
        // Arrange
        var calculator = new StringCalculator();

        // Act
        int result1 = calculator.Calculate("1,2,3");
        int result2 = calculator.Calculate("1\n2\n3");
        int result3 = calculator.Calculate("1,2\n3");
        int result4 = calculator.Calculate("1\n2,3");

        // Assert
        Assert.Equal(6, result1);
        Assert.Equal(6, result2);
        Assert.Equal(6, result3);
        
    }
    
    [Fact]
    public void Calculate_NegativeNumbers_ThrowException()
    {
        //Arrange
        var calculator = new StringCalculator();

        //Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => calculator.Calculate("-3"));
        Assert.Throws<ArgumentOutOfRangeException>(() => calculator.Calculate("1,-2"));
        Assert.Throws<ArgumentOutOfRangeException>(() => calculator.Calculate("4\n-5"));
    }

    [Fact]
    public void Calculate_GreaterThanThousand_Ignored()
    {
        //Arrange
        var calculator = new StringCalculator();
        
        //Act
        int result1 = calculator.Calculate("1,2,10001");
        int result2 = calculator.Calculate("1003,2,1001");
        int result3 = calculator.Calculate("12345,3002,10001");
        
        //Assert
        Assert.Equal(3, result1);
        Assert.Equal(2, result2);
        Assert.Equal(0, result3);
    }
    
    [Fact]
    public void Calculate_CustomDelimiterWithNewLine_ReturnsSum()
    {
        //Arrange
        var calculator = new StringCalculator();

        //Act
        int result = calculator.Calculate("//#\n1#2\n3");

        //Assert
        Assert.Equal(6, result);
    }

    [Fact]
    public void Calculate_CustomDelimiterWithNegativeNumber_ThrowsException()
    {
        //Arrange
        var calculator = new StringCalculator();

        //Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => calculator.Calculate("//#\n2#-3"));
    }

    [Fact]
    public void Calculate_CustomDelimiter_IgnoresNumbersGreaterThan1000()
    {
        //Arrange
        var calculator = new StringCalculator();

        //Act
        int result = calculator.Calculate("//#\n2#1001#3");

        //Assert
        Assert.Equal(5, result);
    }
    
    [Fact]
    public void Calculate_MultiCharacterDelimiter_ReturnSum()
    {
        //Arrange
        var calculator = new StringCalculator();

        //Act
        int result = calculator.Calculate("//[###]\n1###2###3");

        //Assert
        Assert.Equal(6, result);
    }
    
    [Fact]
    public void Calculate_MultiCharacterDelimiter_DifferentDelimiter_ReturnSum()
    {
        //Arrange
        var calculator = new StringCalculator();

        //Act
        int result = calculator.Calculate("//[***]\n2***3***4");

        //Assert
        Assert.Equal(9, result);
    }
    
    [Fact]
    public void Calculate_MultiDelimiter_IgnoresLargeNumbers()
    {
        //Arrange
        var calculator = new StringCalculator();

        //Act
        int result = calculator.Calculate("//[###]\n2###1001###3");

        //Assert
        Assert.Equal(5, result);
    }
    
    [Fact]
    public void Calculate_MultiDelimiter_WithNegative_ThrowsException()
    {
        //Arrange
        var calculator = new StringCalculator();
        
        //Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            calculator.Calculate("//[###]\n1###-2###3"));
    }
    
    [Fact]
    public void Calculate_ManySingleCharDelimiters_ReturnSum()
    {
        // Arrange
        var calculator = new StringCalculator();

        // Act
        int result = calculator.Calculate("//[*][%]\n1*2%3");

        // Assert
        Assert.Equal(6, result);
    }

    [Fact]
    public void Calculate_ManyMultiCharDelimiters_ReturnSum()
    {
        // Arrange
        var calculator = new StringCalculator();

        // Act
        int result = calculator.Calculate("//[###][@@]\n1###2@@3");

        // Assert
        Assert.Equal(6, result);
    }

    [Fact]
    public void Calculate_ManyMixedDelimiters_ReturnSum()
    {
        // Arrange
        var calculator = new StringCalculator();

        // Act
        int result = calculator.Calculate("//[*][###][%]\n1*2###3%4");

        // Assert
        Assert.Equal(10, result);
    }

    [Fact]
    public void Calculate_ManyDelimiters_WithNegative_ThrowsException()
    {
        // Arrange
        var calculator = new StringCalculator();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => calculator.Calculate("//[*][%]\n1*-2%3"));
    }

    [Fact]
    public void Calculate_ManyDelimiters_IgnoresNumbersGreaterThan1000()
    {
        // Arrange
        var calculator = new StringCalculator();

        // Act
        int result = calculator.Calculate("//[***][%%]\n2***1001%%3");

        // Assert
        Assert.Equal(5, result);
    }

    [Fact]
    public void Calculate_ManyDelimiters_WithNewLine_ReturnSum()
    {
        // Arrange
        var calculator = new StringCalculator();

        // Act
        int result = calculator.Calculate("//[*][%]\n1*2\n3%4");

        // Assert
        Assert.Equal(10, result);
    }
}