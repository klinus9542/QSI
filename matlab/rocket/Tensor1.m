function [ ret ] = Tensor1( values, matrix )
    if values(1) == 0
        ret = matrix;
    else
        ret = eye(values(1));
    end
    for num = 2: length(values)
        if values(num) == 0
            ret = kron(ret, matrix);
        else
            ret = kron(ret, eye(values(num)));
        end
    end
end

