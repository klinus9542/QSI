using QuantumRuntime;
using QuantumRuntime.Operator;
using static QuantumRuntime.ControlStatement;
using static QuantumRuntime.Operator.U;
using static QuantumRuntime.Operator.E;
using static QuantumRuntime.Operator.M;
using static QuantumRuntime.Quantum;

namespace UnitTest
{
    class TestQuantumWalk1
    {
        Quantum q1 = new Quantum(2);

        E.Emit e_1 = MakeE("{[1/sqrt(2) 0;0 1/sqrt(2)],[0 1/sqrt(2);1/sqrt(2) 0 ]}"); //This E has two items
        //E.Emit e_2 = MakeE("{[1/sqrt(2) 1/sqrt(2);1/sqrt(2) -1/sqrt(2)]}");  //This E has only item, does it work?
        //E.Emit e_3 = MakeE("{[1/sqrt(2) 1/2;1/2 1/2],[1/2 1/2;1/2 -1/2+i]}");

        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");
        //M.Emit m_2 = MakeM("{[1 0;0 0],[0 0;0 1]}");
        //M.Emit m_3 = MakeM("{[1/2 1/2;1/2 1/2],[1/2 1/2;1/2 -1/2+i],[1/2 1/2;1/2 1/2]}");


        public void Run()
        {
            QWhile(m(q1),
                () =>
                {
                    //e_1(q1);
                }
                );
        }
    }
}