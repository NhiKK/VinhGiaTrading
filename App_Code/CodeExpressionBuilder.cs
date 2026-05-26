using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.CodeDom;

[ExpressionPrefix("Code")]
public class CodeExpressionBuilder : ExpressionBuilder
{
    public override CodeExpression GetCodeExpression(System.Web.UI.BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
    {
        return new CodeSnippetExpression(entry.Expression);
    }
}
