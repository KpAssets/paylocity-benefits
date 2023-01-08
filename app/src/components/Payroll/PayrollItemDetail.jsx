import ListGroup from 'react-bootstrap/ListGroup';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { currencyFormat } from '../../Constants';

/**
 * @param {item: {amount: number, description: string}} props
 * @returns 
 */
const PayrollItemDetail = (props) => {
    return (
        <ListGroup.Item>
            <Container>
                <Row>
                    <Col sm={8}>{props.item.description}</Col>
                    <Col sm={2}>{currencyFormat(props.item.amount)}</Col>
                </Row>
            </Container>
        </ListGroup.Item>
    );
};

export default PayrollItemDetail;