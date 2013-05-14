<?php
/* Compute probabilites for which team*/
$whichTeams= array('mi','rr','rcb','srh','csk');

/* Current number of wins as on the morning of 12th May 2013*/
$curr = array();
$curr['csk'] = 10;
$curr['rr'] = 9;
$curr['mi'] = 9;
$curr['srh'] = 8;
$curr['rcb'] = 8;

/* Upcoming matchess on the morning of 12th May 2013*/
$matches = array();
$matches[]=array('kkr','rcb');
$matches[]=array('rr','csk');
$matches[]=array('mi','srh');
$matches[]=array('rcb','kxip');
$matches[]=array('csk','dd');
$matches[]=array('mi','rr');
$matches[]=array('srh','rr');
$matches[]=array('kxip','mi');
$matches[]=array('rcb','csk');
$matches[]=array('srh','kkr');

$numMatches = count($matches);
$numPossibleOutcomes = pow(2,$numMatches);

//echo "numPossibleOutcomes = $numPossibleOutcomes\n";

$yes = array();
$no = array();
$mayBe = array();

for($possIter = 0; $possIter < $numPossibleOutcomes; $possIter++) {
	$final = $curr;
	for($matchIter = 0; $matchIter < $numMatches; $matchIter++) {
		$winnerIndex = ($possIter >> $matchIter) & 1;	
		$winner = $matches[$matchIter][$winnerIndex]; 
		//echo $winner." ";
		if(isset($final[$winner])) {
			$final[$winner] += 1;
		}
	}
	$finalCopy = $final;
	foreach($whichTeams as $currTeam) {
		if(!isset($yes[$currTeam])) {
			$yes[$currTeam] = 0;
			$no[$currTeam] = 0;
			$mayBe[$currTeam] = 0;
		}
		$numWins = $finalCopy[$currTeam];
		rsort($final);
		if($numWins > $final[4]) { //currTeam has won more than the fifth placed team
			$yes[$currTeam]++;
		} else if($numWins < $final[3]) { //currTeam has won less than the fourth placed team
			$no[$currTeam]++;
		} else { // There is a tie between 4th and 5th place and currTeam is one of them 
			$mayBe[$currTeam]++;
		};

	}
} 

foreach($whichTeams as $currTeam) {
	$yesProb = round(($yes[$currTeam]/$numPossibleOutcomes),3);
	$noProb = round(($no[$currTeam]/$numPossibleOutcomes),3);
	$mayBeProb = round(($mayBe[$currTeam]/$numPossibleOutcomes),3);
	echo "team = $currTeam yes = $yesProb no = $noProb mayBe = $mayBeProb\n";
}
echo memory_get_peak_usage();
?>
