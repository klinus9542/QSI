function PreSKDecomposition( dir, name, matrix, method )
    cont1 = [ 1 0 0 0; 0 1 0 0; 0 0 0 1; 0 0 1 0 ];
    cont2 = [ 1 0 0 0; 0 0 0 1; 0 0 1 0; 0 1 0 0 ];
    dir = [dir, '\\', name];
    mkdir(dir);
    save([dir, '\\value.mat'], 'matrix');
    
    if ~isequal(matrix, cont1) && ~isequal(matrix, cont2)
        [r, c] = size(matrix);
        if r == 2 && c == 2
            mkdir([dir, '\\UnitaryMatLev3']);
            save([dir, '\\UnitaryMatLev3\\matrix_0_0.mat'], 'matrix');
        else
            if method == 0
                [CircuitStructureMatLev3, UnitaryMatLev3] = OrginalQR(matrix);
            elseif method == 1
                [CircuitStructureMatLev3, UnitaryMatLev3] = OrginalQSD(matrix);
            end
            save([dir, '\\CircuitStructureMatLev3.mat'], 'CircuitStructureMatLev3');
            save([dir, '\\UnitaryMatLev3.mat'], 'UnitaryMatLev3');
            [r, c] = size(CircuitStructureMatLev3);
            for i = 1: r
                if any(CircuitStructureMatLev3(i,:)) == 0
                    mkdir([dir, '\\UnitaryMatLev3']);
                    for j = 1: c
                        matrix = cell2mat(UnitaryMatLev3(i, j));
                        if ~isequal(matrix, [])
                            save([dir, '\\UnitaryMatLev3\\matrix_', num2str(i - 1), '_', num2str(j - 1), '.mat'], 'matrix');
                        end
                    end
                end
            end
        end
    end
end

