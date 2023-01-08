import Accordion from 'react-bootstrap/Accordion';
import ListGroup from 'react-bootstrap/ListGroup';
import PayrollCategoryHeader from './PayrollCategoryHeader';
import PayrollItemDetail from './PayrollItemDetail';

/**
 * 
 * @param {{title: string, items: [{amount: number, description: string}] }} props 
 * @returns 
 */
const PayrollCategoryDetail = (props) => {
    return (
        <Accordion.Item eventKey={props.title}>
            <PayrollCategoryHeader title={props.title} items={props.items} />
            <Accordion.Body>
                <ListGroup variant="flush">
                    {props.items.map(item => (
                        <PayrollItemDetail
                            key={`${item.amount}${item.description}`}
                            item={item} />
                    ))}
                </ListGroup>
            </Accordion.Body>
        </Accordion.Item>
    );
};

export default PayrollCategoryDetail;