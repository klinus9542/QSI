function [ ret ] = TensorNot1( values, order, dimension, matrix )
    ret = matrix;
    for num = 1: length(values)
        ret = kron(ret, eye(values(num)));
    end
    order(order) = 1: length(order);
    ret = PermuteSystems(ret, order, dimension);
end