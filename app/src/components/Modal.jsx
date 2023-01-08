import BootstrapModal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';

/**
 * @param {{title: string, isOpen: boolean, onClose: () => {}, canSubmit: boolean, submitButtonName: string, children: any, onSubmit: () => {}}} props
 * @returns
 */
const Modal = (props) => {
    return (
        <BootstrapModal show={props.isOpen} onHide={props.onClose}>
            <BootstrapModal.Header closeButton>
                <BootstrapModal.Title>{props.title}</BootstrapModal.Title>
            </BootstrapModal.Header>
            <BootstrapModal.Body>
                {props.children}
            </BootstrapModal.Body>
            <BootstrapModal.Footer>
                <Button onClick={props.onClose}>Cancel</Button>
                <Button onClick={props.onSubmit}>{props.submitButtonName}</Button>
            </BootstrapModal.Footer>
        </BootstrapModal>
    );
};

export default Modal;