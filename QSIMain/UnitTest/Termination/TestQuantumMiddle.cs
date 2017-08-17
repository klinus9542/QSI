using QuantumRuntime;
using QuantumRuntime.Operator;
using static QuantumRuntime.ControlStatement;
using static QuantumRuntime.Operator.U;
using static QuantumRuntime.Operator.E;
using static QuantumRuntime.Operator.M;
using static QuantumRuntime.Quantum;

namespace UnitTest
{
    class TestQuantumMid1 //concurrent Loop, almost T with T  //test pass 
    {
        Quantum q1 = new Quantum(2);

        E.Emit e_1 = MakeE("{[1/sqrt(2) 0;0 1/sqrt(2)],[0 1/sqrt(2);1/sqrt(2) 0 ]}"); //Bit Flip  p=1/2
        E.Emit e_2 = MakeE("{[0 1;1 0]}"); //Bit Flip p=1

        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        public void run()
        {
            QWhile(m(q1), //Qwhile 1, almost termination 
                () =>
                {
                    e_1(q1);
                }
                );

            QWhile(m(q1), //Qwhile 2, termination
                () =>
                {
                    e_2(q1);
                }
                );
        }
    }

    class TestQuantumMid2 //concurrent Loop, almost T with non-T   
    {
        Quantum q1 = new Quantum(2);

        E.Emit e_1 = MakeE("{[1/sqrt(2) 0;0 1/sqrt(2)],[0 1/sqrt(2);1/sqrt(2) 0 ]}"); //Bit Flip  p=1/2
        E.Emit e_2 = MakeE("{[0 1;1 0]}"); //Bit Flip p=1

        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        public void run()
        {
            QWhile(m(q1), //Qwhile 1, non-termination 
                () =>
                {
                    //e_1(q1);
                }
                );

            QWhile(m(q1), //Qwhile 2, almost termination
                () =>
                {
                    e_1(q1);
                }
                );
        }
    }

    class TestQuantumMid3 //concurrent Loop, T with non-T   
    {
        Quantum q1 = new Quantum(2);

        E.Emit e_1 = MakeE("{[1/sqrt(2) 0;0 1/sqrt(2)],[0 1/sqrt(2);1/sqrt(2) 0 ]}"); //Bit Flip  p=1/2
        E.Emit e_2 = MakeE("{[0 1;1 0]}"); //Bit Flip p=1

        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        public void run()
        {
            QWhile(m(q1), //Qwhile 1, non-termination 
                () =>
                {
                    //e_1(q1);
                }
                );

            QWhile(m(q1), //Qwhile 2, termination
                () =>
                {
                    e_2(q1);
                }
                );
        }
    }

    class TestQuantumMid4 //concurrent Loop. T , non-T  and almost T 
    {
        Quantum q1 = new Quantum(2);

        E.Emit e_1 = MakeE("{[1/sqrt(2) 0;0 1/sqrt(2)],[0 1/sqrt(2);1/sqrt(2) 0 ]}"); //Bit Flip  p=1/2
        E.Emit e_2 = MakeE("{[0 1;1 0]}"); //Bit Flip p=1

        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        public void run()
        {
            QWhile(m(q1), //Qwhile 1, termination
                () =>
                {
                    e_2(q1);
                }
                );

            QWhile(m(q1), //Qwhile 2, almost termination
                () =>
                {
                    e_1(q1);
                }
                );

            QWhile(m(q1), //Qwhile 3, non-termination 
                () =>
                {

                }
                );
        }
    }


    /*---------------------
       //Loop nesting, inherent loop is T
       //Patrial pass
       //Final output matrix should be verified
     ----------------------*/

    class TestQuantumMid5
    {
        Quantum q1 = new Quantum(2);


        E.Emit e_2 = MakeE("{[0 1;1 0]}"); //Bit Flip p=1

        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        public void run()
        {
            QWhile(m(q1),
                () =>
                {
                    QWhile(m(q1), //Qwhile 2, termination
                        () =>
                        {
                            e_2(q1);
                        }
                        );
                }
                );
        }
    }

    /*---------------------
       //Loop nesting, inherent loop is non T
       //Patrial 
       //Final output matrix should be verified
     ----------------------*/

    class TestQuantumMid6
    {
        Quantum q1 = new Quantum(2);

        E.Emit e_1 = MakeE("{[1/sqrt(2) 0;0 1/sqrt(2)],[0 1/sqrt(2);1/sqrt(2) 0 ]}"); //Bit Flip  p=1/2
        E.Emit e_2 = MakeE("{[0 1;1 0]}"); //Bit Flip p=1

        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        public void run()
        {
            QWhile(m(q1),
                () =>
                {
                    QWhile(m(q1), //Qwhile 2, non-termination
                        () =>
                        {
                            //Empty body
                        }
                        );
                }
                );
        }
    }

    /*---------------------
      //Loop nesting, inherent loop is almost T
      //Patrial 
      //Final output matrix should be verified
    ----------------------*/

    class TestQuantumMid7
    {
        Quantum q1 = new Quantum(2);

        E.Emit e_1 = MakeE("{[1/sqrt(2) 0;0 1/sqrt(2)],[0 1/sqrt(2);1/sqrt(2) 0 ]}"); //Bit Flip  p=1/2
        E.Emit e_2 = MakeE("{[0 1;1 0]}"); //Bit Flip p=1

        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        public void run()
        {
            QWhile(m(q1),
                () =>
                {
                    QWhile(m(q1), //Qwhile 2, almost termination
                        () =>
                        {
                            e_1(q1);
                        }
                        );
                }
                );
        }
    }


    /*---------------------
      //Loop nesting, inherent loop is  T and almost T
      //PASS
      //Final output matrix have been verified
    ----------------------*/

    class TestQuantumMid8
    {
        Quantum q1 = new Quantum(2);

        E.Emit e_1 = MakeE("{[1/sqrt(2) 0;0 1/sqrt(2)],[0 1/sqrt(2);1/sqrt(2) 0 ]}"); //Bit Flip  p=1/2
        E.Emit e_2 = MakeE("{[0 1;1 0]}"); //Bit Flip p=1

        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        public void run()
        {
            QWhile(m(q1),
                () =>
                {
                    QWhile(m(q1), //Qwhile 2, almost termination
                        () =>
                        {
                            e_1(q1);
                        }
                        );

                    QWhile(m(q1), //Qwhile 3, termination
                        () =>
                        {
                            e_2(q1);
                        }
                        );

                }
                );
        }
    }

    /*---------------------
   //Loop nesting, inherent loop is  T and almost T
   //Pass
   //Final output matrix has been verified
 ----------------------*/

    class TestQuantumMid9
    {
        Quantum q1 = new Quantum(2);

        E.Emit e_1 = MakeE("{[1/sqrt(2) 0;0 1/sqrt(2)],[0 1/sqrt(2);1/sqrt(2) 0 ]}"); //Bit Flip  p=1/2
        E.Emit e_2 = MakeE("{[0 1;1 0]}"); //Bit Flip p=1

        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        public void run()
        {
            QWhile(m(q1),
                () =>
                {
                    QWhile(m(q1), //Qwhile 2, termination
                        () =>
                        {
                            e_2(q1);
                        }
                        );

                    QWhile(m(q1), //Qwhile 3, almost termination
                        () =>
                        {
                            e_1(q1);
                        }
                        );
                }
                );
        }
    }

    class TestQuantumMid10
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
                },
                () =>
                {
                    e_1(q1);
                }
                );
        }
    }




    /*---------------------
  //Loop nesting, inherent loop is  T and almost T
  //Pass
  //Final output matrix has been verified
----------------------*/

    class TestQuantumMid11
    {
        Quantum q1 = new Quantum(2);

        E.Emit e_1 = MakeE("{[0 1;1 0]}");
        E.Emit e_2 = MakeE("{[1/sqrt(2) 0;0 1/sqrt(2)],[0 1/sqrt(2);1/sqrt(2) 0 ]}"); //Bit Flip  p=1/2
        E.Emit e_3 = MakeE("{[0 1;1 0]}"); //Bit Flip p=1
        E.Emit e_4 = MakeE("{[1 0;0 i]}"); //phase Flip p=1

        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        public void run()
        {
            e_1(q1);

            QWhile(m(q1), //Qwhile2
                () =>
                {
                    QWhile(m(q1), //Qwhile 3
                        () =>
                        {
                            e_2(q1);
                        }
                        );

                    QWhile(m(q1), //Qwhile 4, 
                        () =>
                        {
                            QIf(m(q1), //Qif1
                                () =>
                                {
                                    e_1(q1);
                                },
                                () =>
                                {
                                    e_2(q1);
                                }
                                );
                        }
                        );
                }
                );

            QIf(m(q1), //Qif2
                () =>
                {
                    e_3(q1);
                },
                () =>
                {
                    e_4(q1);
                }
                );

            QWhile(m(q1), //Qwhile 5
                () =>
                {
                    e_2(q1);
                }
                );
        }
    }
}