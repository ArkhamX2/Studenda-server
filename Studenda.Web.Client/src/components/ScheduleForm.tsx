import { FC } from 'react'
import '../styles/schedule.css';
import Day from './UI/day/Day';
import DayName from './UI/dayname/DayName';

const ScheduleForm: FC = () => {

    return (
                <div className="mainBackground">
                        <div style={{display: 'flex', flexDirection: 'column'}}>
                                <div style={{display: 'flex', flexDirection: 'row'}}>
                                        <DayName text1='Понедельник'>
                                                <Day/>
                                        </DayName>
                                        <DayName text1='Понедельник'>
                                                <Day/>
                                        </DayName>
                                </div>
                                <div style={{display: 'flex', flexDirection: 'row'}}>
                                        <DayName text1='Понедельник'>
                                                <Day/>
                                        </DayName>
                                        <DayName text1='Понедельник'> 
                                                <Day/>
                                        </DayName>
                                </div>
                                <div style={{display: 'flex', flexDirection: 'row'}}>
                                        <DayName text1='Понедельник'>
                                                <Day/>
                                        </DayName>
                                        <DayName text1='Понедельник'>
                                                <Day/>
                                        </DayName>
                                        
                                </div>
                        </div>
                       
                        
                </div>

    )
}

export default ScheduleForm