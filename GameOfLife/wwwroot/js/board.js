function toggleCellState(cell) {
    const row = cell.getAttribute('data-row');
    const col = cell.getAttribute('data-col');

    fetch(`/GameOfLife/ToggleCell`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ row: row, col: col })
    })
        .then(response => {
            if (!response.ok) {
                return response.json().then(error => { throw new Error(error.message); });
            }
            return response.json();
        })
        .then(data => {
            if (data.newState === 1) {
                cell.classList.remove('dead');
                cell.classList.add('alive');
            } else {
                cell.classList.remove('alive');
                cell.classList.add('dead');
            }
        })
        .catch(error => {
            showCustomModal('Error', 'Failed to toggle the cell state: ' + error.message);
        });
}

function updateBoard(board) {
    const table = document.querySelector('.board-container table');
    if (!table) {
        showCustomModal('Error', 'Failed to update the board: Table element not found.');
        return;
    }

    for (let i = 0; i < board.length; i++) {
        for (let j = 0; j < board[i].length; j++) {
            const cell = table.rows[i].cells[j];
            if (cell) {
                if (board[i][j] == 1) {
                    cell.classList.add('alive');
                    cell.classList.remove('dead');
                } else {
                    cell.classList.add('dead');
                    cell.classList.remove('alive');
                }
            } else {
                showCustomModal('Error', `Failed to update the board: Cell at position [${i},${j}] not found.`);
                return;
            }
        }
    }
}