%%Decend sorting jordan block
% This function can be called as a way:
%
%   requires: nothing
%   author: Wenyun Sun (anders0821@hotmail.com) and Shusen Liu(klinus@outlook.com)
%   package: termination
%   last updated: July 4, 2016

function [V_dec,J_dec]=decend_jordan(Input_A)
[V, J] = jordan(Input_A);
invV = inv(V);
assert( norm(Input_A-V*J*invV, 'fro') < 1e-6 )

% compute blkSize & eigen
d = diag(J);
blkSize = [];
eigen = [];
for i=1:numel(d)
    if(i==1)
        blkSize(end+1) = 1;
        eigen(end+1) = d(i);
    else
        if d(i)==d(i-1)
           blkSize(end) = blkSize(end)+1;
        else
           blkSize(end+1) = 1;
        eigen(end+1) = d(i);
        end
    end
end
% d
% blkSize
% eigen

% convert to blocks
V2 = mat2cell(V, sum(blkSize), blkSize);
J2 = mat2cell(J, blkSize, blkSize);
invV2 = mat2cell(invV, blkSize, sum(blkSize));
assert( norm(Input_A-cell2mat(V2)*cell2mat(J2)*cell2mat(invV2), 'fro') < 1e-6 )

% resort eigen & blocks
[~,idx] = sort(eigen,'descend');
% eigen = eigen(idx);
% eigen
V2 = V2(1, idx);
J2 = J2(idx, idx);
invV2 = invV2(idx, 1);
V2 = cell2mat(V2);
J2 = cell2mat(J2);
invV2 = cell2mat(invV2);
assert( norm(inv(V2)-invV2, 'fro') < 1e-6 )
assert( norm(Input_A-V2*J2*invV2, 'fro') < 1e-6 )

V_dec=V2;
J_dec=J2;
end