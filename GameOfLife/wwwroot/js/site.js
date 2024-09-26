document.getElementById('randomizeBoard').addEventListener('click', function () {
    fetch('/GameOfLife/RandomizeBoard', {
        method: 'POST',
    })
        .then(response => {
            if (!response.ok) {
                return response.json().then(error => { throw new Error(error.message); });
            }
            return response.json();
        })
        .then(data => {
            if (data.success) {
                updateBoard(data.board);
                updateSessionInformation({
                    generation: data.generation,
                    totalAliveCells: 0,
                    totalDeadCells: 0
                });
            }
        })
        .catch(error => {
            showCustomModal('Error', 'Failed to randomize the board: ' + error.message);
        });
});

document.getElementById('nextGeneration').addEventListener('click', function () {
    fetch('/GameOfLife/NextGeneration', {
        method: 'POST',
    })
        .then(response => {
            if (!response.ok) {
                return response.json().then(error => { throw new Error(error.message); });
            }
            return response.json();
        })
        .then(data => {
            if (data.success) {
                updateBoard(data.board);
                updateSessionInformation(data.sessionInfo);
            }
        })
        .catch(error => {
            showCustomModal('Error', 'Failed to generate the next generation: ' + error.message);
        });
});

document.getElementById('clearBoard').addEventListener('click', function () {
    fetch('/GameOfLife/ClearBoard', {
        method: 'POST',
    })
        .then(response => {
            if (!response.ok) {
                return response.json().then(error => { throw new Error(error.message); });
            }
            return response.json();
        })
        .then(data => {
            if (data.success) {
                updateBoard(data.board);
                updateSessionInformation({
                    generation: 0,
                    totalAliveCells: 0,
                    totalDeadCells: 0,
                    cellsAtRiskOfUnderpopulation: 0,
                    cellsAtRiskOfOvercrowding: 0,
                    willSurvive: 0,
                    willReproduce: 0
                });
            }
        })
        .catch(error => {
            showCustomModal('Error', 'Failed to clear the board: ' + error.message);
        });
});

// Auto Play functionality
let isAutoPlaying = false;
let autoPlayInterval;

document.getElementById('autoPlay').addEventListener('click', function () {
    if (!isAutoPlaying) {
        isAutoPlaying = true;
        autoPlay();
        document.getElementById('autoPlay').textContent = "Stop Auto Play";
    } else {
        stopAutoPlay();
    }
});

function autoPlay() {
    autoPlayInterval = setInterval(() => {
        fetch('/GameOfLife/NextGeneration', {
            method: 'POST',
        })
            .then(response => {
                if (!response.ok) {
                    return response.json().then(error => { throw new Error(error.message); });
                }
                return response.json();
            })
            .then(data => {
                if (data.success) {
                    updateBoard(data.board);
                    updateSessionInformation(data.sessionInfo);

                    if (!data.boardChanged) {
                        stopAutoPlay();
                        showCustomModal('Board Has Stabilized', 'Auto play has been stopped.');
                    }
                }
            })
            .catch(error => {
                stopAutoPlay();
                showCustomModal('Error', 'An error occurred during auto play: ' + error.message);
            });
    }, 500);
}

function stopAutoPlay() {
    isAutoPlaying = false;
    clearInterval(autoPlayInterval);
    document.getElementById('autoPlay').textContent = "Auto Play";
}

function updateSessionInformation(sessionInfo) {
    document.getElementById('generationCount').textContent = sessionInfo.generation;
    document.getElementById('aliveCells').textContent = sessionInfo.totalAliveCells;
    document.getElementById('deadCells').textContent = sessionInfo.totalDeadCells;
    document.getElementById('underpopulationRisk').textContent = sessionInfo.cellsAtRiskOfUnderpopulation;
    document.getElementById('overcrowdingRisk').textContent = sessionInfo.cellsAtRiskOfOvercrowding;
    document.getElementById('willSurvive').textContent = sessionInfo.cellsThatWillSurvive;
    document.getElementById('willReproduce').textContent = sessionInfo.cellsThatWillReproduce;
}