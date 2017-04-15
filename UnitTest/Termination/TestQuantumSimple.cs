using QuantumRuntime;
using QuantumRuntime.Operator;
using static QuantumRuntime.ControlStatement;
using static QuantumRuntime.Operator.U;
using static QuantumRuntime.Operator.E;
using static QuantumRuntime.Operator.M;
using static QuantumRuntime.Quantum;

namespace UnitTest
{
    class TestQuantumSimple1 //test pass
    {
        Quantum q1 = new Quantum(2);
        //E.Emit e_1 = MakeE("{[1/sqrt(2) 0;0 1/sqrt(2)],[0 1/sqrt(2);1/sqrt(2) 0 ]}"); //This E has two items
        E.Emit e_1 = MakeE("{[1/sqrt(2) 1/sqrt(2);1/sqrt(2) -1/sqrt(2)]}");  //H gate
        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        public void run()
        {
            QWhile(m(q1),
                () =>
                {
                    e_1(q1);
                }
                );
        }
    }
    class TestQuantumSimple2  //test pass
    {
        Quantum q1 = new Quantum(2);
        E.Emit e_1 = MakeE("{[1/sqrt(2) 0;0 1/sqrt(2)],[0 1/sqrt(2);1/sqrt(2) 0 ]}"); //Bit flip super-operator.
        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        public void run()
        {
            QWhile(m(q1),
                () =>
                {
                    e_1(q1);
                }
                );
        }
    }

    class TestQuantumSimple3   //test pass
    {
        Quantum q1 = new Quantum(2);
        E.Emit e_1 = MakeE("{[1/sqrt(2) 0;0 1/sqrt(2)],[0 1/sqrt(2);1/sqrt(2) 0 ]}"); //Bit flip super-operator.
        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");
        public void run()
        {
            QWhile(m(q1),
                () =>
                {
                    //e_1(q1);   //Empty body
                }
                );
        }
    }

    class TestQuantumSimple4    //test pass
    {
        Quantum q1 = new Quantum(2);
        E.Emit e_1 = MakeE("{[0 1;1 0 ]}"); //Bit flip super-operator. P=1
        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");
        public void run()
        {
            QWhile(m(q1),
                () =>
                {
                    e_1(q1);
                }
                );
        }
    }

    class TestQuantumSimple5  //test pass  
    {
        Quantum q1 = new Quantum(2);
        E.Emit e_1 = MakeE("{[1/sqrt(2) 0;0 1/sqrt(2)],[1/sqrt(2) 0;0 -1/sqrt(2)]}"); //phase-flip super-operator.
        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");
        public void run()
        {
            QWhile(m(q1),
                () =>
                {
                    e_1(q1);
                }
                );
        }
    }

    class TestQuantumSimple6    //test pass
    {
        Quantum q1 = new Quantum(2);
        E.Emit e_1 = MakeE("{[1 0;0 1/sqrt(2)],[0 0;0 1/sqrt(2)]}"); //phase damping super-operator.
        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");
        public void run()
        {
            QWhile(m(q1),
                () =>
                {
                    e_1(q1);
                }
                );
        }
    }

    class TestQuantumSimple7        //test pass
    {
        Quantum q1 = new Quantum(2);
        E.Emit e_1 = MakeE("{[1 0;0 1/sqrt(2)],[0 1/sqrt(2);0 0]}"); //amplitude damping damping super-operator.
        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");
        public void run()
        {
            QWhile(m(q1),
                () =>
                {
                    e_1(q1);
                }
                );
        }
    }

    class TestQuantumSimple8          //test pass
    {
        Quantum q1 = new Quantum(2);
        E.Emit e_1 = MakeE("{[1/2 0;0 1/2],[0 1/2;1/2 0],[0 -i*1/2;i*1/2 0],[1/2 0;0 -1/2]}"); //depolarizing damping damping super-operator.  p=1
        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");
        public void run()
        {
            QWhile(m(q1),
                () =>
                {
                    e_1(q1);
                }
                );
        }
    }

    class TestQuantumSimple9
    {
        Quantum q1 = new Quantum(2);
        E.Emit e_1 = MakeE("{[1/sqrt(2) 0;0 1/sqrt(2)],[0 1/sqrt(2);1/sqrt(2) 0 ]}"); //depolarizing damping damping super-operator.  p=1
        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");
        public void run()
        {
            QIf(m(q1),
                () =>
                {
                    e_1(q1);
                },
                () =>
                {
                    e_1(q1);
                }
                );
        }
    }
}