import Accordion from 'react-bootstrap/Accordion';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { currencyFormat } from '../../Constants';

const PayrollCategoryHeader = (props) => {
    const getCategorySum = () => {
        return currencyFormat(props.items.reduce((sum, item) => sum + item.amount, 0));
    };

    return (
        <Accordion.Header>
            <Container>
                <Row>
                    <Col sm={8}>{props.title}</Col>
                    <Col sm={2}>{getCategorySum()}</Col>
                </Row>
            </Container>
        </Accordion.Header>
    );
};

export default PayrollCategoryHeader;