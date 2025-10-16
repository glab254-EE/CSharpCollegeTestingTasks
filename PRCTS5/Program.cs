using ConsoleApp2;

Context add = new Context(new Add());
Context sub = new Context(new Substract());
Context mul = new Context(new Multiply());

object firstoperation = sub.ExecuteAlghoritm([10d, 5d ]) ;
double result1 = 0;
Console.WriteLine(firstoperation.ToString());
if (firstoperation is double d)
{
    result1 = d;
}
object secondoperation = add.ExecuteAlghoritm([result1, 25d]);
double result2 = 0;
Console.WriteLine(secondoperation.ToString());
if (secondoperation is double d2)
{
    result2 = d2;
}
object finaloperation = mul.ExecuteAlghoritm([result2, 2.3d]);
Console.WriteLine(finaloperation.ToString());